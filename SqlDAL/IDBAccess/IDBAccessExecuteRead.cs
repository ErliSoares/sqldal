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
using System.Linq;
using System.Threading.Tasks;

namespace System.Data.DBAccess.Generic
{
    public static partial class IDBAccessExtensions
    {
        #region ExecuteRead helpers
        /// <summary>
        /// Prepares the data the populate routine needs to function.
        /// </summary>
        /// <param name="dr">The data row belonging to the table to populate from.</param>
        /// <param name="modelType">The model type to populate.</param>
        /// <returns>A PopulateData object.</returns>
        internal static PopulateData GetPopulateData(this IDBAccess db, DataRow dr, Type modelType)
        {
            return db.GetPopulateData(dr.ItemArray, dr.Table.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToList(), modelType);
        }

        /// <summary>
        /// Prepares the data the populate routine needs to function.
        /// </summary>
        /// <param name="dr">The object array which represents a data row.</param>
        /// <param name="colNames">The list of column names in the same order as the dr.</param>
        /// <param name="modelType">The type of model to populate.</param>
        /// <returns>A PopulateData object.</returns>
        internal static PopulateData GetPopulateData(this IDBAccess db, Object[] dr, List<String> colNames, Type modelType)
        {
            //gets all of the data required by the populate function.
            //this is done here because most of the data required by the Populate function stays the same for every population for a single query

            db.WriteTrace(TraceLevel.DEBUG, "Generating populate data for type: {0}", modelType);

            if (dr == null)
            {
                db.WriteTrace(TraceLevel.DEBUG, "DataRow object was null.");
                return new PopulateData
                {
                    ColCount = 0,
                    ColUpperNames = new List<String>(),
                    HasSetters = new List<Boolean>(),
                    MappedCols = new List<String>(),
                    PropertyFormats = new List<String>(),
                    PropertyTypes = new List<Type>(),
                    FDAIndexes = new List<int>()
                };
            }

            var data = db.ModelsData[modelType];
            //for case insensitivity
            db.WriteTrace(TraceLevel.DEBUG, "Getting column names uppercase.");
            var colUpperNames = colNames.Select(c => c.ToUpper()).ToList();
            int colCount = colNames.Count;

            //returns the property that a column maps to
            //when null, that means the column is not in the model
            db.WriteTrace(TraceLevel.DEBUG, "Getting column mappings from uppercase column names.");
            var mappedCols = colUpperNames.Select(cu =>
            {
                String cumapping = null;
                data.ColumnToModelMappings.TryGetValue(cu, out cumapping);
                return cumapping;
            }).ToList();

            var fda = FastDynamicAccess.Get(modelType);
            var fdaIndexes = mappedCols.Select(c =>
            {
                if (c == null)
                    return 0;
                else
                    return fda.PropertyToArrayIndex[c];
            }).ToList();

            //duplicates SprocParamterNameToModelPropertyName but changes the keys toupper for case insensitivity
            var sprocUppers = data.SprocParamterNameToModelPropertyName.Where(kvp => data.ModelProperties[kvp.Value].PropertyType.DeclaringType == null).ToDictionary(kvp => kvp.Key.ToUpper(), kvp => kvp.Value);

            //list of model properties HasSetter in order that they are returned from the DB
            db.WriteTrace(TraceLevel.DEBUG, "Getting HasSetters for model.");
            var hasSetters = db.GetAllNestedPropertyValues<Boolean>(sprocUppers, colNames, modelType, data, (md, pn) => md.ModelPropertiesAccessors[pn].HasSetter).ToList();

            db.WriteTrace(TraceLevel.DEBUG, "Getting AllNestedPropertyTypes for model.");
            //list of model property types in order that they are returned from the DB
            var propertyTypes = db.GetAllNestedPropertyValues<Type>(sprocUppers, colNames, modelType, data, (md, pn) => md.ModelProperties[pn].PropertyType).ToList();

            db.WriteTrace(TraceLevel.DEBUG, "Getting AllNestedPropertyFormats for model.");
            //list of model properties write stringformats in order that they are returned from the DB
            var propertyFormats = db.GetAllNestedPropertyValues<String>(sprocUppers, colNames, modelType, data, (md, pn) => md.ModelWriteStringFormats[pn]).ToList();

            db.WriteTrace(TraceLevel.DEBUG, "Returning PopulateData object with the following counts:{0}Cols: {1}{0}ColCount: {2}{0}ColUpperNames: {3}{0}MappedCols: {4}{0}HasSetters: {5}{0}PropertyFormats: {6}{0}PropertyTypes: {7}{0}FDA Indexes: {8}{0}",
                                          Environment.NewLine,
                                          colNames.Count,
                                          colCount,
                                          colUpperNames.Count,
                                          mappedCols.Count,
                                          mappedCols.Count,
                                          hasSetters.Count,
                                          propertyFormats.Count,
                                          propertyTypes.Count,
                                          fdaIndexes.Count);
            return new PopulateData
            {
                ColCount = colCount,
                ColUpperNames = colUpperNames,
                MappedCols = mappedCols,
                HasSetters = hasSetters,
                PropertyFormats = propertyFormats,
                PropertyTypes = propertyTypes,
                FDAIndexes = fdaIndexes
            };
        }

        /// <summary>
        /// Gets all nested values of type T
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="colName">The column name to look for.</param>
        /// <param name="data">The ModelData object to start with.</param>
        /// <param name="valueSelector">The function which returns the value once found.</param>
        /// <returns>The value as a T.</returns>
        private static T GetNestedPopulateValue<T>(this IDBAccess db, String colName, ModelData data, Func<ModelData, String, T> valueSelector)
        {
            foreach (var nest in data.NestedModelBaseFields)
            {
                var type = nest.Value.FieldType;
                db.WriteTrace(TraceLevel.DEBUG, "Searching type {0}", type);
                var thisData = db.ModelsData[type];
                var sprocUppers = thisData.SprocParamterNameToModelPropertyName.Where(kvp => thisData.ModelProperties[kvp.Value].PropertyType.DeclaringType == null).ToDictionary(kvp => kvp.Key.ToUpper(), kvp => kvp.Value);
                String propertyName;
                foreach (var p in thisData.ModelFields)
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Trying property {0}", p.Key);
                    // if it's a nested model, recursively call
                    if (thisData.NestedModelBaseFields.ContainsKey(p.Key))
                    {
                        db.WriteTrace(TraceLevel.DEBUG, "Property is a nested type.  Making recursive call to GetNestedPopulateValue.");
                        return db.GetNestedPopulateValue<T>(colName, thisData, valueSelector);
                    }
                    //else if it has it, get it
                    else if (sprocUppers.TryGetValue(colName.ToUpper(), out propertyName))
                    {
                        T value = valueSelector(thisData, propertyName);
                        db.WriteTrace(TraceLevel.DEBUG, "Returning value of {0}", value);
                        return value;
                    }
                }
            }

            T def = default(T);
            db.WriteTrace(TraceLevel.DEBUG, "Returning default value of {0}.", def);
            return def;
        }

        /// <summary>
        /// Gets all nested property values of type T.
        /// </summary>
        /// <typeparam name="T">The type of values.</typeparam>
        /// <param name="sprocUppers">The sproc parameter name to model property name mappings but with the sproc names capitalized.</param>
        /// <param name="colNames">List of column names returned by the data set.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="data">The ModelData object for the model type.</param>
        /// <param name="valueSelector">The function which returns the value once found.</param>
        /// <returns>An enumeration of type T.</returns>
        internal static IEnumerable<T> GetAllNestedPropertyValues<T>(this IDBAccess db, Dictionary<String, String> sprocUppers, List<String> colNames, Type modelType, ModelData data, Func<ModelData, String, T> valueSelector)
        {
            foreach (String c in colNames)
            {
                db.WriteTrace(TraceLevel.DEBUG, "Getting nested values for column {0} in model type {1}", c, modelType);
                //if our model contains this column
                if (sprocUppers.ContainsKey(c.ToUpper()))
                {
                    //and it's not a nested property of this model, return the write stringformat
                    if (!data.NestedModelBaseFields.ContainsKey(c))
                    {
                        T value = valueSelector(data, sprocUppers[c.ToUpper()]);
                        db.WriteTrace(TraceLevel.DEBUG, "Returning value of {0}.", value);
                        yield return value;
                    }

                }
                else // else try to get it from a nested model
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Searching in nested models.");
                    yield return db.GetNestedPopulateValue<T>(c, data, valueSelector);
                }
            }
        }

        /// <summary>
        /// Gets all property types in the supplied type and all nested types within it.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <returns>An enumeration of types representing all types found within a model.</returns>
        internal static IEnumerable<Type> GetAllNestedTypes(this IDBAccess db, Type modelType)
        {
            db.WriteTrace(TraceLevel.DEBUG, "Getting all nested types in type {0}", modelType);
            foreach (var t in db.ModelsData[modelType].NestedModelBaseFields.Values)
            {
                db.WriteTrace(TraceLevel.DEBUG, "Returning type {0}", t.FieldType);
                yield return t.FieldType;

                foreach (var it in db.GetAllNestedTypes(t.FieldType))
                    yield return it;
            }
        }

        /// <summary>
        /// Populates an enumeration of the specified type using the supplied data rows using a SqlDataReader.
        /// </summary>
        /// <typeparam name="T">The type of objects to create.</typeparam>
        /// <param name="tuple">The tuple object containing the data rows, column names and column types.</param>
        /// <param name="parentChildPropertyName">Optional parameter specifying the property name in T's parent type which will be a list of T.</param>
        /// <returns>The list of populated objects of type T.</returns>
        internal static List<T> PopulateModelBaseEnumeration<T>(this IDBAccess db, ExecuteReadQuickTuple tuple, List<String> parentChildPropertyName = null)
            where T : class, new()
        {
            var icp = db as ICustomPopulate;
            if (icp != null)
                return icp.PopulateModelBaseEnumeration<T>(tuple, parentChildPropertyName);
            else
                return db.PopulateModelBaseEnumerationPrivate<T>(tuple, parentChildPropertyName);
        }

        private static List<T> PopulateModelBaseEnumerationPrivate<T>(this IDBAccess db, ExecuteReadQuickTuple tuple, List<String> parentChildPropertyName = null)
            where T : class, new()
        {
            // if generic parameter is object, this means we are creating a type at runtime
            if (typeof(T) == typeof(Object))
            {
                return db.PopulateModelBaseEnumeration(tuple, typeof(T), parentChildPropertyName).OfType<T>().ToList();
            }

            var dataRows = tuple.DataRows;

            db.WriteTrace(TraceLevel.DEBUG, "Populating model base enumeration of size {0} with type {1}, populateDefaultValues {2}, parentChildPropertyName {3}", dataRows.Count, typeof(T), db.PopulateDefaultValues, parentChildPropertyName);
            //duplicated function for speed
            //in this function we start by creating a T, otherwise we would have to call the Object version below and cast to a T afterwards

            Type modelType = typeof(T);

            if (!db.ModelsData.ContainsKey(modelType))
            {
                db.WriteTrace(TraceLevel.DEBUG, "Validating type for DAL usage.");
                db.ValidateForDAL(new T());
            }

            ModelData data = db.ModelsData[modelType];

            var drf = dataRows.FirstOrDefault();

            var pData = db.GetPopulateData(drf, tuple.ColumnNames, modelType);

            db.WriteTrace(TraceLevel.DEBUG, "Getting all nested populate data objects.");
            var allNestedPData = db.GetAllNestedTypes(modelType).ToDictionary(t => t, t => db.GetPopulateData(drf, tuple.ColumnNames, t));

            int numItems = dataRows.Count;
            List<T> retList = new List<T>(numItems);

            var propertyNames = new List<String>();
            var stringFormats = new List<String>();
            var propertyTypes = new List<Type>();

            for (int i = 0; i < pData.MappedCols.Count; i++)
            {
                if (pData.MappedCols[i] != null)
                {
                    propertyNames.Add(pData.MappedCols[i]);
                    stringFormats.Add(pData.PropertyFormats[i]);
                    propertyTypes.Add(pData.PropertyTypes[i]);
                }
            }

            var del = FastDynamicAccess.GetModelPopulateMethod(propertyNames, stringFormats, propertyTypes, modelType);

            if (db.IsMultiThreaded)
            {
                db.WriteTrace(TraceLevel.DEBUG, "Multithreaded using {0} threads", db.Threads);
                if ((db.PopulateDefaultValues) && !modelType.DerivesInterface(typeof(IQuickPopulate)))
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Populating default values.");
                    var dVData = db.GetDefaultValuesToPopulate(data, pData.ColUpperNames);
                    var allNestedDVData = db.GetAllNestedTypes(modelType).ToDictionary(t => t, t => db.GetDefaultValuesToPopulate(db.ModelsData[t], pData.ColUpperNames));

                    return dataRows.AsParallel().AsOrdered().WithDegreeOfParallelism(db.Threads).Select(dr =>
                    {
                        T t = new T();
                        del(t, dr);
                        //db.Populate(t, dr, data, modelType, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
                        db.PopulateDefaultModelValues(t, dVData, modelType, pData.ColUpperNames, data, allNestedDVData);
                        return t;
                    }).ToList();
                }
                else if (modelType.DerivesInterface(typeof(IQuickPopulate)))
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Populating with IQuickPopulate.");
                    var indexes = new Dictionary<String, int>();
                    for (int i = 0; i < tuple.ColumnNames.Count; i++)
                        indexes.Add(tuple.ColumnNames[i], i);

                    return dataRows.AsParallel().AsOrdered().WithDegreeOfParallelism(db.Threads).Select(dr =>
                    {
                        T t = new T();
                        ((IQuickPopulate)t).DALPopulate(dr, indexes);
                        return t;
                    }).ToList();
                }
                else
                {
                    return dataRows.AsParallel().AsOrdered().WithDegreeOfParallelism(db.Threads).Select(dr =>
                    {
                        T t = new T();
                        del(t, dr);
                        //db.Populate(t, dr, data, modelType, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
                        return t;
                    }).ToList();
                }
            }
            else
            {
                db.WriteTrace(TraceLevel.DEBUG, "Single threaded.");
                if ((db.PopulateDefaultValues) && !modelType.DerivesInterface(typeof(IQuickPopulate)))
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Populating default values.");
                    var dVData = db.GetDefaultValuesToPopulate(data, pData.ColUpperNames);
                    var allNestedDVData = db.GetAllNestedTypes(modelType).ToDictionary(t => t, t => db.GetDefaultValuesToPopulate(db.ModelsData[t], pData.ColUpperNames));

                    for (int i = 0; i < numItems; i++)
                    {
                        T t = new T();
                        del(t, dataRows[i]);
                        //db.Populate(t, dataRows[i], data, modelType, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
                        db.PopulateDefaultModelValues(t, dVData, modelType, pData.ColUpperNames, data, allNestedDVData);
                        retList.Add(t);
                    }

                    return retList;
                }
                else if (modelType.DerivesInterface(typeof(IQuickPopulate)))
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Populating with IQuickPopulate.");
                    var indexes = new Dictionary<String, int>();
                    for (int i = 0; i < tuple.ColumnNames.Count; i++)
                        indexes.Add(tuple.ColumnNames[i], i);

                    for (int i = 0; i < numItems; i++)
                    {
                        T t = new T();
                        ((IQuickPopulate)t).DALPopulate(dataRows[i], indexes);
                        retList.Add(t);
                    }

                    return retList;
                }
                else
                {
                    for (int i = 0; i < numItems; i++)
                    {
                        T t = new T();
                        del(t, dataRows[i]);
                        //db.Populate(t, dataRows[i], data, modelType, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
                        retList.Add(t);
                    }

                    return retList;
                }
            }
        }

        /// <summary>
        /// Populates an enumeration of the specified type using the supplied data rows.  This differs from the generic function in that this is used for return sets larger than 16 where a generic argument is not possible.
        /// </summary>
        /// <param name="dataRows">The data rows with which to populate.</param>
        /// <param name="modelType">The type of objects to create.</param>
        /// <param name="parentChildPropertyName">Optional parameter specifying the property name in the parent model's type which will be a list of Object.</param>
        /// <returns>The list of populated objects.</returns>
        internal static List<Object> PopulateModelBaseEnumeration(this IDBAccess db, ExecuteReadQuickTuple tuple, Type modelType, List<String> parentChildPropertyName = null)
        {
            var icp = db as ICustomPopulate;
            if (icp != null)
                return icp.PopulateModelBaseEnumeration(tuple, modelType, parentChildPropertyName);
            else
                return db.PopulateModelBaseEnumerationPrivate(tuple, modelType, parentChildPropertyName);
        }

        internal static List<Object> PopulateModelBaseEnumerationPrivate(this IDBAccess db, ExecuteReadQuickTuple tuple, Type modelType, List<String> parentChildPropertyName = null)
        {
            var dataRows = tuple.DataRows;
            db.WriteTrace(TraceLevel.DEBUG, "Populating model base enumeration of size {0} with type {1}, populateDefaultValues {2}, parentChildPropertyName {3}", dataRows.Count, modelType, db.PopulateDefaultValues, parentChildPropertyName);
            // if generic parameter is object, this means we are creating a type at runtime
            if (modelType == typeof(Object))
            {
                db.WriteTrace(TraceLevel.INFORMATION, "Model type is object.  Populating with a runtime class.");
                return db.PopulateModelBaseEnumeration(tuple, dataRows.GetRuntimeType(tuple.TableName, tuple.ColumnNames, tuple.ColumnTypes, parentChildPropertyName));
            }

            if (!db.ModelsData.ContainsKey(modelType))
            {
                db.WriteTrace(TraceLevel.DEBUG, "Validating type for DAL usage.");
                db.ValidateForDAL(Activator.CreateInstance(modelType));
            }

            ModelData data = db.ModelsData[modelType];

            var drf = dataRows.FirstOrDefault();
            if (drf == null)
                return new List<Object>();

            var pData = db.GetPopulateData(drf, tuple.ColumnNames, modelType);

            db.WriteTrace(TraceLevel.DEBUG, "Getting all nested populate data objects.");
            var allNestedPData = db.GetAllNestedTypes(modelType).ToDictionary(t => t, t => db.GetPopulateData(drf, tuple.ColumnNames, t));

            if (db.IsMultiThreaded)
            {
                db.WriteTrace(TraceLevel.DEBUG, "Multithreaded using {0} threads", db.Threads);
                if ((db.PopulateDefaultValues) && !modelType.DerivesInterface(typeof(IQuickPopulate)))
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Populating default values.");
                    var dVData = db.GetDefaultValuesToPopulate(data, pData.ColUpperNames);
                    var allNestedDVData = db.GetAllNestedTypes(modelType).ToDictionary(t => t, t => db.GetDefaultValuesToPopulate(db.ModelsData[t], pData.ColUpperNames));

                    return dataRows.AsParallel().AsOrdered().WithDegreeOfParallelism(db.Threads).Select(dr =>
                    {
                        Object t = Activator.CreateInstance(modelType);
                        db.Populate(t, dr, data, modelType, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
                        db.PopulateDefaultModelValues(t, dVData, modelType, pData.ColUpperNames, data, allNestedDVData);
                        return t;
                    }).ToList();
                }
                else if (modelType.DerivesInterface(typeof(IQuickPopulate)))
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Populating with IQuickPopulate.");
                    var indexes = new Dictionary<String, int>();
                    for (int i = 0; i < tuple.ColumnNames.Count; i++)
                        indexes.Add(tuple.ColumnNames[i], i);

                    return dataRows.AsParallel().AsOrdered().WithDegreeOfParallelism(db.Threads).Select(dr =>
                    {
                        Object t = Activator.CreateInstance(modelType);
                        ((IQuickPopulate)t).DALPopulate(dr, indexes);
                        return t;
                    }).ToList();
                }
                else
                {
                    return dataRows.AsParallel().AsOrdered().WithDegreeOfParallelism(db.Threads).Select(dr =>
                    {
                        Object t = Activator.CreateInstance(modelType);
                        db.Populate(t, dr, data, modelType, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
                        return t;
                    }).ToList();
                }
            }
            else
            {
                db.WriteTrace(TraceLevel.DEBUG, "Single threaded.");
                int numItems = dataRows.Count;
                List<Object> retList = new List<Object>(numItems);

                if ((db.PopulateDefaultValues) && !modelType.DerivesInterface(typeof(IQuickPopulate)))
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Populating default values.");
                    var dVData = db.GetDefaultValuesToPopulate(data, pData.ColUpperNames);
                    var allNestedDVData = db.GetAllNestedTypes(modelType).ToDictionary(t => t, t => db.GetDefaultValuesToPopulate(db.ModelsData[t], pData.ColUpperNames));

                    for (int i = 0; i < numItems; i++)
                    {
                        Object t = Activator.CreateInstance(modelType);
                        db.Populate(t, dataRows[i], data, modelType, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
                        db.PopulateDefaultModelValues(t, dVData, modelType, pData.ColUpperNames, data, allNestedDVData);
                        retList.Add(t);
                    }

                    return retList;
                }
                else if (modelType.DerivesInterface(typeof(IQuickPopulate)))
                {
                    db.WriteTrace(TraceLevel.DEBUG, "Populating with IQuickPopulate.");
                    var indexes = new Dictionary<String, int>();
                    for (int i = 0; i < tuple.ColumnNames.Count; i++)
                        indexes.Add(tuple.ColumnNames[i], i);

                    for (int i = 0; i < numItems; i++)
                    {
                        Object t = Activator.CreateInstance(modelType);
                        ((IQuickPopulate)t).DALPopulate(dataRows[i], indexes);
                        retList.Add(t);
                    }

                    return retList;
                }
                else
                {
                    for (int i = 0; i < numItems; i++)
                    {
                        Object t = Activator.CreateInstance(modelType);
                        db.Populate(t, dataRows[i], data, modelType, pData.MappedCols, pData.ColUpperNames, pData.ColCount, pData.HasSetters, pData.PropertyTypes, pData.PropertyFormats, allNestedPData, pData.FDAIndexes);
                        retList.Add(t);
                    }

                    return retList;
                }
            }
        }
        #endregion

        /// <summary>
        /// Executes a read operation against a SQL database using the provided options.
        /// </summary>
        /// <typeparam name="T">The type of output models to return.</typeparam>
        /// <returns>An enumeration of output models of type T.</returns>
        public static List<T> ExecuteRead<T>(this IDBAccess db)
            where T : class, new()
        {
            return db.PopulateModelBaseEnumeration<T>(db.ExecuteReadQuick());
        }

        /// <summary>
        /// Executes a read operation against a SQL database using the provided options and a SqlDataReader object.
        /// </summary>
        /// <returns>An enumeration of the raw DataRow objects.</returns>
        private static ExecuteReadQuickTuple ExecuteReadQuick(this IDBAccess db)
        {
            return db.ExecuteReadQuick();
        }

        #region ExecuteReadAsync
        private static void ExecuteReadCallback(this IDBAccess db, DataTable dt, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadCallbackDelegate callback)
        {
            if (callback != null)
                callback(dt);
        }

        private static void ExecuteReadCallback<T>(this IDBAccess db, List<T> rows, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadCallbackDelegate<T> callback)
            where T : class, new()
        {
            if (callback != null)
                callback(rows);
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database using the provided options.
        /// </summary>
        /// <typeparam name="T">The type of output models to return.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type List T.  If this is null, no callback will be made.</param>
        public static void ExecuteReadAsync<T>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadCallbackDelegate<T> callback = null)
            where T : class, new()
        {
            new Task(() =>
            {
                var rows = db.PopulateModelBaseEnumeration<T>(db.ExecuteReadQuick());
                db.ExecuteReadCallback<T>(rows, callback);
            }).Start();
        }
        #endregion

        #region ExecuteReadSingle
        /// <summary>
        /// Executes a read operation against a SQL database using the provided options and returns the first row from the result.
        /// </summary>
        /// <typeparam name="T">The type of output models to return.</typeparam>
        /// <returns>The tuple object containing the data row, column names and column types.</returns>
        private static ExecuteReadQuickTuple ExecuteReadSingleQuick(this IDBAccess db)
        {
            var tuple = db.ExecuteReadQuick();
            if (tuple.DataRows.Any())
            {
                var objs = new List<Object[]> { tuple.DataRows[0] };
                tuple.DataRows = objs;
            }
            return tuple;
        }

        /// <summary>
        /// Executes a read operation against a SQL database using the provided options.
        /// </summary>
        /// <typeparam name="T">The type of output models to return.</typeparam>
        /// <returns>The first row of the result converted to an object of type T.  Null is returned if the data set was empty.</returns>
        public static T ExecuteReadSingle<T>(this IDBAccess db)
            where T : class, new()
        {
            var row = db.ExecuteReadSingleQuick();
            if (row.DataRows == null || !row.DataRows.Any())
                return null;

            return db.PopulateModelBaseEnumeration<T>(row).First();
        }
        #endregion

        #region ExecuteReadSingleAsync
        private static void ExecuteReadSingleCallback(this IDBAccess db, DataRow row, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadSingleCallbackDelegate callback)
        {
            if (callback != null)
                callback(row);
        }

        private static void ExecuteReadSingleCallback<T>(this IDBAccess db, T row, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadSingleCallbackDelegate<T> callback)
            where T : class, new()
        {
            if (callback != null)
                callback(row);
        }

        /// <summary>
        /// Executes an asynchronous read operation against a SQL database using the provided options.
        /// </summary>
        /// <typeparam name="T">The type of output models to return.</typeparam>
        /// <param name="callback">The callback function to execute after the query is complete.  The function takes one parameter of type T.  If this is null, no callback will be made.</param>
        public static void ExecuteReadSingleAsync<T>(this IDBAccess db, System.Data.DBAccess.Generic.AsyncDelegates.ExecuteReadSingleCallbackDelegate<T> callback = null)
            where T : class, new()
        {
            new Task(() =>
            {
                var row = db.ExecuteReadSingle<T>();
                db.ExecuteReadSingleCallback<T>(row, callback);
            }).Start();
        }
        #endregion
    }
}