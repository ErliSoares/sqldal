/*
Copyright 2012 Brian Adams

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.Collections.Generic;
using System.Data.DBAccess.Generic.Exceptions;
using System.Linq;
using System.Reflection;

namespace System.Data.DBAccess.Generic
{
    #region ModelBase
    /// <summary>
    /// Class which represents cached data about types used by the DAL.
    /// </summary>
    public class ModelData
    {
        internal Dictionary<String, FieldInfo> ModelFields;  // delete?
        internal Dictionary<String, PropertyInfo> ModelProperties; // delete?
        internal List<String> ModelPropertiesNames;
        internal Dictionary<String, FieldInfo> AllNestedModelFields; // delete?
        internal List<String> AllNestedModelPropertyNames;
        internal List<String> ModelFieldsNames; // delete?
        internal Dictionary<String, String> ModelFieldsSprocParameterNames;
        internal Dictionary<String, String> AllNestedModelFieldsSprocParameterNames;
        internal Dictionary<String, String> ModelWriteStringFormats;
        internal Dictionary<String, String> ModelReadStringFormats;
        internal Dictionary<String, ParameterDirection> ModelParameterDirections;
        internal Dictionary<DataTable, List<String>> ModelFieldsNotReturnedFromDT;
        internal Dictionary<String, Boolean> NullableModelFields;
        internal Dictionary<String, Object> ModelFieldsDefaultValues;
        internal Dictionary<String, FieldInfo> NestedModelBaseFields; // delete?
        internal Dictionary<String, PropertyAccessors> ModelPropertiesAccessors;
        internal Dictionary<String, String> SprocParamterNameToModelPropertyName;
        internal Dictionary<String, String> ColumnToModelMappings;
        internal FastDynamicAccess FastDynamicAccess;
        internal Dictionary<Type, Boolean> NestedTypesInstantiatedInConstructor;
    }

    //internal "ModelBase" functionality for the DAL
    public static partial class IDBAccessExtensions
    {
        /// <summary>
        /// Validates the type of the passed in object can be used by the DAL.  If it cannot an exception will be thrown.  This method constructs the ModelData cache for this object's type.
        /// </summary>
        /// <param name="model">The object.</param>
        public static void ValidateForDAL(this IDBAccess db, Object model)
        {
            if (db.ModelsData.ContainsKey(model.GetType())) return;

            //find all nested models and verify them first starting with the innermost
            foreach (var modelType in db.GetNestedModelTypes(model).Reverse().Where(m => !db.ModelsData.ContainsKey(m)))
                db.ValidateForDAL(Activator.CreateInstance(modelType));

            db.ValidateForDAL(model.GetType());
        }

        /// <summary>
        /// Validates a type can be used by the DAL.  If it cannot an exception will be thrown.  This method constructs the ModelData cache for this type.
        /// </summary>
        /// <param name="modelType">The type.</param>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if duplicate SQL parameter names are defined.</exception>
        /// <exception cref="ModelPropertyInvalidException">Thrown if duplicate SQL parameter names are defined using a case insensitive compare.</exception>
        private static void ValidateForDAL(this IDBAccess db, Type modelType)
        {
            lock (db.ModelsData)
            {
                if (db.ModelsData.ContainsKey(modelType))
                    return;

                var data = new ModelData();

                data.ModelFields = modelType.GetProperties()
                    .Select(p =>
                        new
                        {
                            PropertyName = p.Name,
                            Field = db.GetFieldByPropertyName(modelType, p.Name, data)
                        }).ToDictionary(p => p.PropertyName, p => p.Field);

                data.ModelProperties = modelType.GetProperties().ToDictionary(p => p.Name, p => p);
                data.ModelPropertiesNames = modelType.GetProperties().Select(p => p.Name).ToList();

                data.ModelFieldsNames = data.ModelFields.Select(p => p.Key).ToList();

                data.ModelFieldsSprocParameterNames = data.ModelPropertiesNames
                                                            .ToDictionary(p => p, p =>
                                                            {
                                                                var attr = db.GetPropertyAttribute<DALSQLParameterNameAttribute>(modelType, p, data);
                                                                return (attr != null) ? attr.Name : p;
                                                            });

                try
                {
                    data.SprocParamterNameToModelPropertyName = data.ModelPropertiesNames.Where(p => db.IsInputToSproc(modelType, p, data))
                                                                .ToDictionary(p =>
                                                                {
                                                                    var attr = db.GetPropertyAttribute<DALSQLParameterNameAttribute>(modelType, p, data);
                                                                    return (attr != null) ? attr.Name : p;
                                                                }, p => p);
                }
                catch (ArgumentException aex)
                {
                    if (aex.Message == "An item with the same key has already been added.")
                    {
                        var duplicates = data.ModelPropertiesNames.Where(p => db.IsInputToSproc(modelType, p, data)).GroupBy(p =>
                        {
                            var attr = db.GetPropertyAttribute<DALSQLParameterNameAttribute>(modelType, p, data);
                            return (attr != null) ? attr.Name : p;
                        }).Where(g => g.Count() > 1).Select(g => g.Key);
                        throw new ModelPropertyMisconfiguredException(String.Format("Duplicate Sproc parameter names defined: {0}", String.Join(", ", duplicates)));
                    }
                }

                data.ModelWriteStringFormats = data.ModelPropertiesNames.ToDictionary(p => p, p =>
                {
                    var attr = db.GetPropertyAttribute<DALWriteStringFormatAttribute>(modelType, p, data);
                    return (attr != null) ? attr.Format : null;
                });

                data.ModelReadStringFormats = data.ModelPropertiesNames.ToDictionary(p => p, p =>
                {
                    var attr = db.GetPropertyAttribute<DALReadStringFormatAttribute>(modelType, p, data);
                    return (attr != null) ? attr.Format : null;
                });

                data.ModelParameterDirections = data.ModelPropertiesNames.ToDictionary(p => p, p =>
                {
                    var attr = db.GetPropertyAttribute<DALParameterDirectionAttribute>(modelType, p, data);
                    return (attr != null) ? attr.Direction : ParameterDirection.Input;
                });

                data.ModelFieldsNotReturnedFromDT = new Dictionary<DataTable, List<String>>();

                data.NullableModelFields = modelType.GetProperties().ToDictionary(p => p.Name, p => p.PropertyType.IsNullableValueType());

                data.ModelFieldsDefaultValues = data.ModelPropertiesNames
                    .Where(p => db.GetPropertyAttribute<DALDefaultValueAttribute>(modelType, p, data) != null)
                    .ToDictionary(p => p, p =>
                    {
                        var attr = db.GetPropertyAttribute<DALDefaultValueAttribute>(modelType, p, data);
                        return attr.Value;
                    });

                data.NestedModelBaseFields = modelType.GetProperties()
                    .Where(p => db.IsNestedProperty(modelType, p.Name))
                    .Select(p => new { Name = p.Name, Field = db.GetFieldByPropertyName(modelType, p.Name, data) })
                    .ToDictionary(p => p.Name, p => p.Field);

                //find internal/public property getters and setters
                data.ModelPropertiesAccessors = modelType.GetProperties().ToDictionary(p => p.Name, p =>
                    new PropertyAccessors
                    {
                        HasGetter = p.GetAccessors(true).Any(a =>
                            a.Name.StartsWith("get_") &&
                            (a.Attributes.ToString().ToLower().Contains("public") || a.Attributes.ToString().ToLower().Contains("assembly"))),

                        HasSetter = p.GetAccessors(true).Any(a =>
                            a.Name.StartsWith("set_") &&
                            (a.Attributes.ToString().ToLower().Contains("public") || a.Attributes.ToString().ToLower().Contains("assembly")))
                    });

                db.AssertModelIsValid(data, modelType);

                //has to come after because of dictionary collisions if the model is not valid
                //building these caches also performs some additional validation
                Dictionary<Type, List<String>> dict = new Dictionary<Type, List<String>>();
                db.PopulateNestedModelPropertiesDictionary(modelType, dict, data);

                //flatten all lists of model properties from the nested types
                var many = dict.SelectMany(kvp => kvp.Value);
                data.AllNestedModelFields =
                    many.Select(p => new { Name = p, Field = db.GetFieldByPropertyName(modelType, p, data) })
                    .ToDictionary(p => p.Name, p => p.Field);

                data.AllNestedModelFieldsSprocParameterNames = data.AllNestedModelFields.Select(p => p.Key)
                                                                    .ToDictionary(p => p, p =>
                                                                    {
                                                                        var attr = db.GetPropertyAttribute<DALSQLParameterNameAttribute>(modelType, p, data);
                                                                        return (attr != null) ? attr.Name : p;
                                                                    });

                data.AllNestedModelPropertyNames = data.AllNestedModelFieldsSprocParameterNames.Select(kvp => kvp.Key).ToList();

                //determine which nested properties are instantiated in the constructor... ones that are we should not re-create when populating this parent model type
                try
                {
                    Object m = Activator.CreateInstance(modelType);
                    data.NestedTypesInstantiatedInConstructor = data.NestedModelBaseFields.ToDictionary(p => p.Value.FieldType, p => m.GetValue(p.Key) != null);
                }
                //occurs when the model type is an anonymous type.  in which case any nested types cannot be instantiated in the anonymous constructor so ignore these cases
                catch (MissingMethodException) { }

                //assert there are no case insensitive property conflicts
                var duplicateCaseInsensitivity = data.AllNestedModelPropertyNames.GroupBy(p => p.ToUpper()).Where(g => g.Count() > 1).Select(g => g.Key);
                if (duplicateCaseInsensitivity.Any())
                    throw new ModelPropertyInvalidException(String.Format("The following model SQL property name case insensitive conflicts were found: {0}",
                        String.Join(",", duplicateCaseInsensitivity)));

                data.ColumnToModelMappings = data.ModelFields.Where(f => db.IsInputToSproc(modelType, f.Key, data) && data.ModelPropertiesAccessors[f.Key].HasSetter).ToDictionary(p => data.ModelFieldsSprocParameterNames[p.Key].ToUpper(), p => p.Key);

                data.FastDynamicAccess = FastDynamicAccess.Get(modelType);

                db.ModelsData.Add(modelType, data);
            }
        }

        #region Model Validation
        /// <summary>
        /// Asserts a model type is valid for use with the DAL.
        /// </summary>
        /// <param name="data">The ModelData object for this modelType.</param>
        /// <param name="modelType">The type.</param>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if there are duplicate SQL parameter names defined.</exception>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if a property contains the WriteStringFormat attribute and is not a string.</exception>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if a property's default value is set to null.</exception>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if any property default value types do not match the type of the property.</exception>
        /// <exception cref="ModelPropertyInvalidException">Thrown if a model property is an enumeration of a disallowed type.</exception>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if the type of any property is a circular reference back to the declaring type.</exception>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if the there are multiple properties with the same SQL parameter name when searching all nested models of this type.</exception>
        /// <exception cref="TableQuickReadMisconfiguredException">Thrown if the length of the array returned by ToObjectArray does not match the number of entries returned by GetColumnNamesTypes in an IQuickRead UDTable type.</exception>
        /// <exception cref="TableQuickReadMisconfiguredException">Thrown if any types returned by GetColumnNamesTypes are a nullable value type in an IQuickREad UDTable table.</exception>
        private static void AssertModelIsValid(this IDBAccess db, ModelData data, Type modelType)
        {
            //validate the configuration of model

            //check to make sure the same sql parameter name isn't defined more than once
            //check to make sure there are no instances of a parameter name and a sql param
            //attribute being the same, or no two attributes are the same
            //ignore cases where the property name is the same as the sql param name
            var sprocParamNamesToConsider = data.ModelFieldsSprocParameterNames.Where(p => p.Key != p.Value && data.ModelParameterDirections[p.Key] == ParameterDirection.Input && db.IsInputToSproc(modelType, p.Key, data)).Select(p => p.Value);
            var allSprocParameterNames = data.ModelFieldsNames.Where(p => data.ModelParameterDirections[p] == ParameterDirection.Input && db.IsInputToSproc(modelType, p, data)).ToList();
            allSprocParameterNames.AddRange(sprocParamNamesToConsider);
            var duplicates = allSprocParameterNames.GroupBy(p => p).Where(pg => pg.Count() > 1).Select(pg => pg.Key);

            if (duplicates.Any())
                throw new ModelPropertyMisconfiguredException(String.Format("Duplicate Sproc parameter names defined: {0}", String.Join(", ", duplicates)));

            //assert all properties with the StringFormat attribute are Strings
            var invalidStringFormats = data.ModelWriteStringFormats.Where(p => p.Value != null && db.GetPropertyType(modelType, p.Key, data) != typeof(String)).Select(p => p.Key);
            if (invalidStringFormats.Any())
                throw new ModelPropertyMisconfiguredException(String.Format("Model properties which contain the WriteStringFormat attribute must be strings.  Please correct the type of the following properties: {0}", invalidStringFormats));

            //assert no default values are null
            var nullDefaultValues = data.ModelFieldsDefaultValues.Where(p => p.Value == null);
            if (nullDefaultValues.Any())
                throw new ModelPropertyMisconfiguredException(String.Format("Model property default values should not be set to null.  Any nullable property will already be nulled by default. Please correct the following properties: {0}", nullDefaultValues.Select(p => p.Key)));

            //assert all default values are actually assignable to the properties they are attached to
            List<String> defaultValuesErrors = new List<String>();
            foreach (var p in data.ModelFieldsDefaultValues)
            {
                Type pType = db.GetPropertyType(modelType, p.Key, data);
                String propertyName = p.Key;
                if (!pType.IsAssignableFrom(p.Value.GetType()))
                {
                    defaultValuesErrors.Add(String.Format("Default value with type '{0}' cannot be assigned to the type '{1}' (model '{2}' property '{3}')", p.Value.GetType(), pType, modelType, propertyName));
                }
            }

            if (defaultValuesErrors.Any())
                throw new ModelPropertyMisconfiguredException(String.Format("Default values validation failed:{0}{1}", Environment.NewLine, String.Join(Environment.NewLine, defaultValuesErrors)));

            //must also validate that any data table (IEnumerable<UDTable>) or DataTable properties are not set to output parameters
            var outputProps =
                (from d in data.ModelParameterDirections.Where(d => d.Value == ParameterDirection.Output)
                 join p in data.ModelProperties on d.Key equals p.Key
                 select p);

            IEnumerable<String> outputParametersErrors = outputProps.Where(p => p.Value.PropertyType == typeof(DataTable) || p.Value.PropertyType.IsUDTableEnumeration())
                .Select(p => String.Format("Output parameter '{0}' cannot be declared as a DataTable or enumertion of UDTable", p.Key));

            if (outputParametersErrors.Any())
                throw new ModelPropertyMisconfiguredException(String.Join(Environment.NewLine, outputParametersErrors));

            //prohibit properties to be enumerable types other than enumerations of UDTables and Byte arrays
            IEnumerable<String> enumerationNotUDTable = modelType.GetProperties()
                .Where(p => p.PropertyType != typeof(String) &&
                    p.PropertyType.GetInterfaces().Any(i => i == typeof(System.Collections.IEnumerable)) &&
                    !p.PropertyType.IsUDTableEnumeration() &&
                    p.PropertyType.GetIEnumerableGenericType() != typeof(Byte) &&
                    p.PropertyType.GetIEnumerableGenericType() != typeof(DALRuntimeTypeBase) &&
                    !p.PropertyType.GetIEnumerableGenericType().IsUserType() &&
                    db.IsInputToSproc(modelType, p.Name, data))
                .Select(p => String.Format("Parameter '{0}' cannot be declared as an enumerable type without being an enumeration of UDTable types or a byte array.", p.Name));

            if (enumerationNotUDTable.Any())
                throw new ModelPropertyInvalidException(String.Join(Environment.NewLine, enumerationNotUDTable));

            if (modelType.DerivesInterface(typeof(IQuickPopulate)))
                return; // QuickPopulate uses the user defined Populate routine, so the user can specify how to populate circular references

            //prohibit nested model types from having circular references
            var circularReferences = db.GetAllNestedModelBaseTypeConflicts(modelType, data);
            if (circularReferences.Any())
                throw new ModelPropertyMisconfiguredException(String.Join(Environment.NewLine, circularReferences));

            //prohibit nested properties from have the same name
            //properties considered: input to sproc, also consider actual sproc property names
            var nestedModelPropertiesDictionary = new Dictionary<Type, List<String>>();
            db.PopulateNestedModelPropertiesDictionary(modelType, nestedModelPropertiesDictionary, data);
            var nestedPropertyConflicts = nestedModelPropertiesDictionary.SelectMany(kvp => kvp.Value).GroupBy(p => p).Where(pg => pg.Count() > 1).Select(p => p.Key);
            if (nestedPropertyConflicts.Any())
            {
                throw new ModelPropertyMisconfiguredException(String.Join(Environment.NewLine,
                    nestedPropertyConflicts
                    .Select(p =>
                        new
                        {
                            Property = p,
                            Models = nestedModelPropertiesDictionary.Keys.Where(k => nestedModelPropertiesDictionary[k].Contains(p))
                        })
                    .Where(p => p.Models.Count() > 1)
                    .Select(p => String.Format("The property '{0}' is declared in multiple models: '{1}'", p.Property, String.Join(", ", p.Models)))));
            }

            if (modelType.DerivesInterface(typeof(IQuickRead)))
            {
                var m = (IQuickRead)Activator.CreateInstance(modelType);
                var objs = m.ToObjectArray();
                var types = m.GetColumnNamesTypes();

                // validate ToObjectArray length matches GetObjectTypes length
                if (objs.Length != types.Count)
                    throw new TableQuickReadMisconfiguredException(String.Format("ToObjectArray length '{0}' does not match GetColumnNamesTypes length '{1}'", objs.Length, types.Count));

                // validate none of the types are nullable
                if (types.Any(t => t.Value.IsNullableValueType()))
                    throw new TableQuickReadMisconfiguredException(String.Format("The following types are nullable value types and should be changed to their corresponding value types: {0}", String.Join(",", types.Where(t => t.Value.IsNullableValueType()))));
            }
        }

        /// <summary>
        /// Returns any conflicts where a property type derives back up to the parent type of the model in any way.
        /// </summary>
        /// <param name="modelType">The parent model type.</param>
        /// <param name="data">The ModelData object for this type.</param>
        /// <returns>An enumeration of error messages.  If nothing returns, the model passes.</returns>
        private static IEnumerable<String> GetAllNestedModelBaseTypeConflicts(this IDBAccess db, Type modelType, ModelData data)
        {
            foreach (var p in modelType.GetProperties().Where(p => p.PropertyType.IsUserType() && p.DeclaringType != modelType && modelType.DerivesFromType(p.DeclaringType)))
            {
                yield return String.Format("The property '{0}' of type '{1}' derives from its parent model of type '{2}'.", p.Name, p.PropertyType, modelType);
            }

            //for each nested property
            foreach (var p in db.GetSprocInputNamesTypes(modelType, data).Where(p => db.IsNestedProperty(modelType, p.Key)))
            {
                //see if it is a user type and if it derives from parent
                if (p.Value.IsUserType() && p.Value.DerivesFromType(modelType))
                {
                    yield return String.Format("The property '{0}' of type '{1}' derives from its parent model of type '{2}'.", p.Key, p.Value, modelType);
                    continue;
                }

                //if not, find all types in model.
                //check each type individually so we can return the full list of conflicts
                var modelBaseTypes = new List<Type>();
                foreach (var t in db.GetNestedModelTypes(p.Value))
                {
                    if (modelBaseTypes.Contains(t))
                    {
                        yield return String.Format("Property '{0}' is a circular reference back to type in which it is declared '{1}'. Reference found underneath '{2}'.", p.Key, t.DeclaringType, modelType);
                        break;
                    }
                    else
                        modelBaseTypes.Add(t);
                }
            }
        }

        /// <summary>
        /// Creates a dictionary of types and all SQL parameter names.
        /// </summary>
        /// <param name="modelType">The type.</param>
        /// <param name="dict">The dictionary.</param>
        /// <param name="data">The ModelData object of the type.</param>
        private static void PopulateNestedModelPropertiesDictionary(this IDBAccess db, Type modelType, Dictionary<Type, List<String>> dict, ModelData data)
        {
            if (dict.ContainsKey(modelType)) return;

            dict.Add(modelType, new List<String>());

            //get a dictionary of all input properties' names and their types
            foreach (var p in db.GetSprocInputNamesTypes(modelType, data)
                    .Select(kvp => new KeyValuePair<String, Type>(data.ModelFieldsSprocParameterNames[kvp.Key], kvp.Value)))
            {
                //if the type is a nested model, populate it's nested properties recursively
                if ((db.ModelsData.ContainsKey(p.Value) && db.IsNestedProperty(modelType, p.Key)))
                {
                    Object m = Activator.CreateInstance(p.Value);
                    db.ValidateForDAL(m);

                    if (m != null)
                        db.PopulateNestedModelPropertiesDictionary(p.Value, dict, db.ModelsData[p.Value]);
                }
                //else add it to the dictionary
                else
                    dict[modelType].Add(p.Key);
            }
        }
        #endregion

        #region Reflection methods
        /// <summary>
        /// Get all model types which are nested types in the model type.
        /// </summary>
        /// <param name="modelType">The parent type.</param>
        /// <returns>An enumeration of all nested model types in this type.</returns>
        internal static IEnumerable<Type> GetTopNestedModelTypes(this IDBAccess db, Type modelType)
        {
            return modelType.GetProperties()
                .Where(p => p.DeclaringType == modelType && p.PropertyType.IsUserType() &&
                            Attribute.GetCustomAttribute(p, typeof(DALIgnoreAttribute)) == null) //ignore DALIgnore properties
                .Select(p => p.PropertyType);
        }

        /// <summary>
        /// Uses GetTopNestedModelTypes to get all nested model types in a model.
        /// </summary>
        /// <param name="modelType">The parent type.</param>
        /// <returns>An enumeration of all nested types contained within the model.</returns>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if any properties in the model are of the same type as the model.</exception>
        internal static IEnumerable<Type> GetNestedModelTypes(this IDBAccess db, Type modelType)
        {
            foreach (Type t in db.GetTopNestedModelTypes(modelType))
            {
                List<String> errors = new List<String>();
                foreach (var n in t.GetProperties().Where(p => p.PropertyType == modelType))
                    errors.Add(String.Format("The property '{0}' of type '{1}' has a circular reference to its parent model of type '{2}'.", n.Name, t.Name, modelType));

                if (errors.Any())
                    throw new ModelPropertyMisconfiguredException(String.Join(Environment.NewLine, errors));

                yield return t;

                foreach (var ti in db.GetNestedModelTypes(t))
                    yield return ti;
            }
        }

        /// <summary>
        /// Uses GetTopNestedModelTypes to get all the nested model types in an object's type.
        /// </summary>
        /// <param name="model">The object.</param>
        /// <returns>An enumeration of all nested types contained within the object type.</returns>
        /// <exception cref="ModelPropertyMisconfiguredException">Thrown if any properties in the model are of the same type as the model.</exception>
        internal static IEnumerable<Type> GetNestedModelTypes(this IDBAccess db, Object model)
        {
            return db.GetNestedModelTypes(model.GetType());
        }

        /// <summary>
        /// Gets a field by the property name.
        /// </summary>
        /// <param name="modelType">The type containing the field.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="data">The ModelData associated with the model type.</param>
        /// <returns>The FieldInfo object.</returns>
        internal static FieldInfo GetFieldByPropertyName(this IDBAccess db, Type modelType, String propertyName, ModelData data)
        {
            //property with custom getter/setter, field can't be used
            if (modelType.GetField(String.Format("<{0}>k__BackingField", propertyName), BindingFlags.Instance | BindingFlags.NonPublic) == null
                && modelType.GetProperties().Any(p => p.Name == propertyName))
                return null;

            return modelType.GetField(String.Format("<{0}>k__BackingField", propertyName), BindingFlags.Instance | BindingFlags.NonPublic)
                // model doesn't have that property, find it in a nested model
                ?? db.FindNestedPropertyByName(modelType, propertyName, data).DeclaringType.GetField(String.Format("<{0}>k__BackingField", propertyName), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Finds a property in a model, searching nested models if necessary.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="data">The ModelData associated with the model type.</param>
        /// <returns>The PropertyInfo object.</returns>
        private static PropertyInfo FindNestedPropertyByName(this IDBAccess db, Type modelType, String propertyName, ModelData data)
        {
            var hasProp = data.ModelFieldsSprocParameterNames.ContainsValue(propertyName);
            PropertyInfo prop = null;

            if (!hasProp)
            {
                foreach (var p in modelType.GetProperties().Where(p => db.IsNestedProperty(modelType, p.Name)))
                {
                    prop = db.FindNestedPropertyByName(p.PropertyType, propertyName, db.ModelsData[p.PropertyType]);
                    if (prop != null) break;
                }
            }
            else
            {
                // lookup the real property name (assume is the sproc name, not the model property name)
                String pName = data.ModelFieldsSprocParameterNames.First(kvp => kvp.Value == propertyName).Key;
                prop = modelType.GetProperties().First(p => propertyName == data.ModelFieldsSprocParameterNames[p.Name]);
            }

            return prop;
        }

        /// <summary>
        /// Gets a dictionary of sql parameter names and their types from a model typel.
        /// </summary>
        /// <param name="modelType">The type.</param>
        /// <param name="data">The ModelData object for the type.</param>
        /// <returns>The dictionary.</returns>
        internal static Dictionary<String, Type> GetSprocInputNamesTypes(this IDBAccess db, Type modelType, ModelData data)
        {
            return db.GetInputProperties(modelType, data).ToDictionary(p => p.Name, p => p.PropertyType);
        }

        /// <summary>
        /// Gets all properties which will be used as input to a SQL statement.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="data">The ModelData object for the type.</param>
        /// <returns>An enumeration of PropertyInfo objects.</returns>
        internal static IEnumerable<PropertyInfo> GetInputProperties(this IDBAccess db, Type modelType, ModelData data)
        {
            return modelType.GetProperties().Where(p => db.IsInputToSproc(modelType, p.Name, data) && data.ModelPropertiesAccessors[p.Name].HasGetter);
        }

        /// <summary>
        /// Gets a dictionary of SQL parameter names and their values for a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The dictionary.</returns>
        internal static Dictionary<String, Object> GetSprocInputNamesValues(this IDBAccess db, Object model)
        {
            Type modelType = model.GetType();
            var data = db.ModelsData[modelType];
            return db.GetInputProperties(modelType, data).ToDictionary(p => p.Name, p => p.GetValue(model, null));
        }

        /// <summary>
        /// Converts a model to an object array of its property values.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="data">The ModelData object associated with the type.</param>
        /// <param name="modelPropertyNames">List of property names in the model in the order that they appear when iterating through them.</param>
        /// <param name="modelPropertyFormats">List of model property ReadStringFormats in the order that they appear when iterating through them.</param>
        /// <returns>An object array of all property values.</returns>
        internal static Object[] ToObjectArray(this IDBAccess db, Object model, Type modelType, ModelData data, List<String> modelPropertyNames, List<String> modelPropertyFormats)
        {
            Object[] retArray = new Object[modelPropertyNames.Count];
            var fda = data.FastDynamicAccess;

            for (int i = 0; i < modelPropertyNames.Count; i++)
            {
                retArray[i] = db.GetValue(model, modelType, data, fda, modelPropertyNames[i], modelPropertyFormats[i]);
            }

            return retArray;
        }

        /// <summary>
        /// Gets a model property value and applies a ReadStringFormat if there is one.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="data">The ModelData object associated with the type.</param>
        /// <param name="fda">The FastDynamicAccess object for the model type.</param>
        /// <param name="modelPropertyName">The model property name.</param>
        /// <param name="modelPropertyFormat">The model property ReadStringFormat.</param>
        /// <returns>The value.</returns>
        internal static Object GetValue(this IDBAccess db, Object model, Type modelType, ModelData data, FastDynamicAccess fda, String modelPropertyName, String modelPropertyFormat)
        {
            Object value = fda.Get(model, modelPropertyName);

            if (modelPropertyFormat != null)
                value = String.Format(modelPropertyFormat, value);

            return value;
        }

        /// <summary>
        /// Sets a model property's value.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name to set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="data">The ModelData object associated with the model type.</param>
        /// <param name="hasSetter">True/False if this property has a setter.</param>
        /// <param name="propertyType">The property type.</param>
        /// <param name="propertyFormat">The property's write String.Format.</param>
        /// <exception cref="ModelPropertyNotNullableException">Thrown if the value is null but the model property cannot be null.</exception>
        /// <exception cref="ModelPropertyColumnMismatchException">Thrown if the value cannot be assigned to the type of the property.</exception>
        internal static void SetValue(this IDBAccess db, Object model, Type modelType, String propertyName, Object value, ModelData data, Boolean hasSetter, Type propertyType, String propertyFormat, int fdaIndex)
        {
            if (!hasSetter)
                return;

            // if property format exists, format the value here.
            // pad an extra null to the format string, because if the value is null, the string format will fail
            if (propertyFormat != null) value = String.Format(propertyFormat, value, null);

            if (value == null || value == DBNull.Value)
            {
                // if the db value is null and the property type cannot accept nulls

                //TO DO: The result of this expression can be cached and passed in
                if (propertyType.IsValueType && !db.IsNullableValueType(modelType, propertyName, data))
                    throw new ModelPropertyNotNullableException(String.Format("Cannot assign null to the non nullable property '{0}'", propertyName));

                data.FastDynamicAccess.Set(model, fdaIndex, null);

                return;
            }
            /*else if (!propertyType.IsAssignableFrom(value.GetType()) && propertyFormat == null)
            {
                //property type does not match the type of value and there is no String.Format defined on the property
                throw new ModelPropertyColumnMismatchException(String.Format("Object passed with type '{0}' cannot be assigned to the type '{1}' (model '{2}' property '{3}')", value.GetType(), propertyType, modelType, propertyName));
            }*/
            else
            {
                try
                {
                    data.FastDynamicAccess.Set(model, fdaIndex, value);
                }
                catch (InvalidCastException)
                {
                    throw new ModelPropertyColumnMismatchException(String.Format("Object passed with type '{0}' cannot be assigned to the type '{1}' (model '{2}' property '{3}')", value.GetType(), propertyType, modelType, propertyName));
                }
            }
        }

        /// <summary>
        /// Returns if the property is a nullable value type.  Uses the ModelData cache.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="data">The ModelData object associated with the property.</param>
        /// <returns>True/False.</returns>
        internal static Boolean IsNullableValueType(this IDBAccess db, Type modelType, String propertyName, ModelData data)
        {
            return data.NullableModelFields[propertyName];
        }

        /// <summary>
        /// Gets the underlying value of a nullable value type.  SLOW.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="property">The property to read.</param>
        /// <param name="data">The ModelData object associated with the object.</param>
        /// <returns>The object.</returns>
        internal static Object GetNullableValueTypeValue(this IDBAccess db, Object model, String property, ModelData data)
        {
            Type modelType = model.GetType();
            FieldInfo pInfo = data.ModelFields[property];

            Object pInfoValue = pInfo.GetValue(model);
            if (pInfoValue == null) return null; // if the nullable type is null

            PropertyInfo value = pInfo.FieldType.GetProperty("Value");
            return value.GetValue(pInfoValue, null);
        }

        /// <summary>
        /// Gets a model property's type.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="data">The ModelData object associated with the model type.</param>
        /// <returns>The type.</returns>
        internal static Type GetPropertyType(this IDBAccess db, Type modelType, String propertyName, ModelData data)
        {
            if (data == null)
                data = db.ModelsData[modelType];

            var f = data.ModelFields[propertyName];

            if (f == null && data.ModelPropertiesAccessors != null && data.ModelPropertiesAccessors[propertyName].HasGetter)
                return data.ModelProperties[propertyName].PropertyType;

            return f.FieldType;
        }

        /// <summary>
        /// Populates a model with a given data row.
        /// </summary>
        /// <param name="model">The model to populate.</param>
        /// <param name="dr">The data row containg the values to populate.</param>
        /// <param name="data">The ModelData object associated with the model type.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="mappedCols">The model property names that correspond to each column.</param>
        /// <param name="colUpperNames">The column names capitalized.</param>
        /// <param name="colCount">The number of columns.</param>
        /// <param name="hasSetters">The HasSetters list for the model property names.</param>
        /// <param name="propertyTypes">The types associated with the property names.</param>
        /// <param name="propertyFormats">The write String.Format string format strings.</param>
        /// <param name="allNestedPData">All nested PopulateData objects.</param>
        /// <param name="fdaIndexes">List of property indexes to use for setting via the FDA object.</param>
        internal static void Populate(this IDBAccess db, Object model, DataRow dr, ModelData data, Type modelType, List<String> mappedCols, List<String> colUpperNames, int colCount, List<Boolean> hasSetters, List<Type> propertyTypes, List<String> propertyFormats, Dictionary<Type, PopulateData> allNestedPData, List<int> fdaIndexes)
        {
            db.Populate(model, dr.ItemArray, data, modelType, mappedCols, colUpperNames, colCount, hasSetters, propertyTypes, propertyFormats, allNestedPData, fdaIndexes);
        }

        /// <summary>
        /// Populates a model with a given data row.
        /// </summary>
        /// <param name="model">The model to populate.</param>
        /// <param name="dr">The data row containg the values to populate.</param>
        /// <param name="data">The ModelData object associated with the model type.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="mappedCols">The model property names that correspond to each column.</param>
        /// <param name="colUpperNames">The column names capitalized.</param>
        /// <param name="colCount">The number of columns.</param>
        /// <param name="hasSetters">The HasSetters list for the model property names.</param>
        /// <param name="propertyTypes">The types associated with the property names.</param>
        /// <param name="propertyFormats">The write String.Format string format strings.</param>
        /// <param name="allNestedPData">All nested PopulateData objects.</param>
        /// <param name="fdaIndexes">List of property indexes to use for setting via the FDA object.</param>
        internal static void Populate(this IDBAccess db, Object model, Object[] dr, ModelData data, Type modelType, List<String> mappedCols, List<String> colUpperNames, int colCount, List<Boolean> hasSetters, List<Type> propertyTypes, List<String> propertyFormats, Dictionary<Type, PopulateData> allNestedPData, List<int> fdaIndexes)
        {
            FastDynamicAccess.GetModelPopulateMethod(mappedCols, propertyFormats, propertyTypes, model.GetType())(model, dr);

            foreach (var nest in data.NestedModelBaseFields)
            {
                //TO DO: this runs about 7% slower checking each time here
                //this is something that would need to be cached in ModelData... which nested types of a model are constructed on instantiation...

                //if the nested model object already exists
                //(if the parent object creates this nested object in the constructor)
                //don't create a new one...
                var thisType = nest.Value.FieldType;

                //if the nested type is instantiated in the constructor of the model parameter, then use the already instantiated value
                Object m = data.NestedTypesInstantiatedInConstructor[nest.Value.FieldType] ? model.GetValue(nest.Key) : Activator.CreateInstance(thisType);
                var thisData = db.ModelsData[thisType];

                var pData = allNestedPData[thisType];
                db.Populate(m, dr, thisData, thisType, pData.MappedCols, colUpperNames, colCount, hasSetters, propertyTypes, propertyFormats, allNestedPData, pData.FDAIndexes);
                data.FastDynamicAccess.Set(model, nest.Key, m);
            }
        }

        /// <summary>
        /// Gets property names and their values of model properties that will take on their default value as a result of the query.
        /// </summary>
        /// <param name="data">The ModelData object associated with the model type.</param>
        /// <param name="colNames">The list of column names.</param>
        /// <returns>A dictioary of property names and their default values.</returns>
        internal static Dictionary<String, Object> GetDefaultValuesToPopulate(this IDBAccess db, ModelData data, List<String> colNames)
        {
            var propertiesNotInDB = data.ModelFieldsNames.Select(cn => cn).Except(colNames).ToList();
            var defaultValuesToPopulate = data.ModelFieldsDefaultValues.Select(dv => dv.Key).Intersect(propertiesNotInDB).ToList();

            return defaultValuesToPopulate.Where(p => data.ModelParameterDirections[p] == ParameterDirection.Input).ToDictionary(p => p, p => data.ModelFieldsDefaultValues[p]);
        }

        /// <summary>
        /// Populates a model's default properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="defaultValuesToPopulate">The dictionary of default properties to populate from the call to GetDefaultValuesToPopulate.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="colNames">The column names.</param>
        /// <param name="data">The ModelData object associated with the model type.</param>
        /// <param name="allNestedDefaultValuesToPopulate">Dictionary mapping the model type to a defaultValuesToPopulate dictionary for all nested types in this model.</param>
        internal static void PopulateDefaultModelValues(this IDBAccess db, Object model, Dictionary<String, Object> defaultValuesToPopulate, Type modelType, List<String> colNames, ModelData data, Dictionary<Type, Dictionary<String, Object>> allNestedDefaultValuesToPopulate)
        {
            //ignore output parameters
            var fda = data.FastDynamicAccess;
            foreach (var kvp in defaultValuesToPopulate)
            {
                fda.Set(model, kvp.Key, kvp.Value);
            }

            foreach (var nest in data.NestedModelBaseFields)
            {
                Type thisType = nest.Value.FieldType;
                ModelData thisData = db.ModelsData[thisType];
                Object m = Activator.CreateInstance(thisType);
                var nestedDefaultValuesToPopulate = allNestedDefaultValuesToPopulate[thisType];
                db.PopulateDefaultModelValues(m, nestedDefaultValuesToPopulate, thisType, colNames, thisData, allNestedDefaultValuesToPopulate);
                fda.Set(model, nest.Key, m);
            }
        }
        #endregion

        #region Property Attributes
        /// <summary>
        /// Gets a model property's SQL parameter direction.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The ParameterDirection.</returns>
        internal static ParameterDirection GetPropertyDirection(this IDBAccess db, Type modelType, String propertyName)
        {
            return db.ModelsData[modelType].ModelParameterDirections[propertyName];
        }

        /// <summary>
        /// Gets a model proerty's write String.Format format string.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The write String.Format format string.</returns>
        internal static String GetPropertyFormat(this IDBAccess db, Type modelType, String propertyName)
        {
            return db.ModelsData[modelType].ModelWriteStringFormats[propertyName];
        }

        // this isn't cached becuse it's only used on input to SQL
        // so the overhead of another dictionary is not worth it
        /// <summary>
        /// Gets whether or not a model property should be ignored for input/output to SQL statements.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="data">The ModelData object associated with the model type.</param>
        /// <returns>True/False.</returns>
        internal static Boolean IsInputToSproc(this IDBAccess db, Type modelType, String propertyName, ModelData data)
        {
            return db.GetPropertyAttribute<DALIgnoreAttribute>(modelType, propertyName, data) == null;
        }

        /// <summary>
        /// Gets a model property's SQL parameter name.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The SQL parameter name.</returns>
        internal static String GetPropertySprocParameterName(this IDBAccess db, Type modelType, String propertyName)
        {
            return db.ModelsData[modelType].ModelFieldsSprocParameterNames[propertyName];
        }

        /// <summary>
        /// Returns whether or not a property is a nested model.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <param name="property">The property.</param>
        /// <returns>True/False.</returns>
        internal static Boolean IsNestedProperty(this IDBAccess db, Type modelType, String property)
        {
            //for nested types which do not get serialized as the whole type (nested properties are flattened)
            var p = modelType.GetProperty(property);
            if (p != null)
                return p.PropertyType.IsUserType();

            //for nested types which do get serialized as the whole type (ex. web services parameters)
            ModelData data;
            if (!db.ModelsData.TryGetValue(modelType, out data))
                return false;

            return modelType.GetProperty(data.SprocParamterNameToModelPropertyName[property]).PropertyType.IsUserType();
        }

        /// <summary>
        /// Gets a DAL property attribute.
        /// </summary>
        /// <typeparam name="T">The type of attribute.</typeparam>
        /// <param name="modelType">The model type.</param>
        /// <param name="property">The property.</param>
        /// <param name="data">The ModelData object associated with the model type.</param>
        /// <returns>The custom attribute of type T.</returns>
        internal static T GetPropertyAttribute<T>(this IDBAccess db, Type modelType, String property, ModelData data) where T : Attribute
        {
            if (data == null) data = db.ModelsData[modelType];
            //first check this type
            var prop = (modelType.GetProperty(property));

            //not found, try to get it from the nested types
            if (prop == null && data.AllNestedModelFields != null && data.AllNestedModelFields.ContainsKey(property))
                prop = db.FindNestedPropertyByName(modelType, property, data);

            //still null, check this property for a property whose sproc param name == property
            else if (prop == null && data.ModelFieldsSprocParameterNames != null && data.ModelFieldsSprocParameterNames.Any(sp => sp.Value == property))
                prop = modelType.GetProperty(data.ModelFieldsSprocParameterNames.First(sp => sp.Value == property).Key);

            //still null, check nested property sproc param names
            else if (prop == null && data.AllNestedModelFieldsSprocParameterNames != null && data.AllNestedModelFieldsSprocParameterNames.Any(kvp => kvp.Value == property))
                prop = db.FindNestedPropertyByName(modelType, property, data);

            if (prop == null)
                return null;
            else
                return Attribute.GetCustomAttribute(prop, typeof(T)) as T;
        }
        #endregion
    }
    #endregion

    /// <summary>
    /// Specifies the direction to be used when creating a SQL parameter from this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DALParameterDirectionAttribute : Attribute
    {
        /// <summary>
        /// The ParameterDirection of the property.  Defaults to input.
        /// </summary>
        public ParameterDirection Direction { get; set; }
    }

    /// <summary>
    /// String.Format to be applied when writing a value into this property.  This attribute may only be used on Strings.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DALWriteStringFormatAttribute : Attribute
    {
        /// <summary>
        /// The String.Format string to apply when reading from the database into this property.
        /// </summary>
        public String Format { get; set; }
    }

    /// <summary>
    /// String.Format to be applied when reading this property into a SQL Parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DALReadStringFormatAttribute : Attribute
    {
        /// <summary>
        /// The String.Format string to apply when reading from this property into a SqlParameter.
        /// </summary>
        public String Format { get; set; }
    }

    /// <summary>
    /// Declares that a property should be ignored by the DAL.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DALIgnoreAttribute : Attribute
    { }

    /// <summary>
    /// If the SQL statement is expecting a different name for this property, or if it will return this property under a different column name, set that name in this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DALSQLParameterNameAttribute : Attribute
    {
        /// <summary>
        /// When creating Sql parameters to use for the query, the name to use when reading this property.
        /// When populating a model from a query, the column name from which to read when populating this property.
        /// When reading into a table parameter type, the column name in which to write when reading this property.
        /// </summary>
        public String Name { get; set; }
    }

    /// <summary>
    /// Value to be used when populating the model in cases where the SQL statement did not return this column and the populate default values option was used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DALDefaultValueAttribute : Attribute
    {
        /// <summary>
        /// The default value to set this property to if the select query does not contain this column.
        /// </summary>
        public Object Value { get; set; }
    }
}