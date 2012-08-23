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
using System.Data.DBAccess.Generic.Providers.SQL;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Data.DBAccess.Generic.Exceptions;

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Utility functions for converting an enumeration of data rows to a List of classes created at runtime.
    /// </summary>
    public static class RuntimeClassConverter
    {
        static RuntimeClassConverter()
        {
            RuntimeClassConverter.s_createdTypes = new Dictionary<String, Type>();
        }

        /// <summary>
        /// Converts an anonymous type to a runtime type for serialization purposes.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>A runtime type object.</returns>
        internal static Object ToNonAnonymousType(this Object obj)
        {
            var fda = FastDynamicAccess.Get(obj);
            var propertyNames = fda.PropertyToArrayIndex.Select(p => p.Key).ToList();
            var propertyValueArray = fda.PropertyToArrayIndex.Select(p => fda.Get(obj, p.Value)).ToArray();
            var propertyTypes = propertyValueArray.Select(o =>
            {
                var type = o.GetType();

                if (type.IsIEnumerable() && (o as System.Collections.IEnumerable).GetIEnumerableGenericType().IsAnonymousType())
                {
                    return typeof(DALRuntimeTypeList);
                }
                else if (type.IsAnonymousType())
                {
                    return o.ToNonAnonymousType().GetType();
                }
                else
                {
                    return type;
                }
            }).ToList();

            var newType = CreateType(null, new List<Object[]> { propertyValueArray }, propertyNames, propertyTypes, null);
            var newFDA = FastDynamicAccess.Get(newType);
            var retObject = Activator.CreateInstance(newType);

            for (int i = 0; i < propertyValueArray.Length; i++)
            //+1 because of the TableName property which comes first in all runtime types
            {
                var value = propertyValueArray[i];
                var type = value.GetType();

                if (type.IsIEnumerable() && (value as System.Collections.IEnumerable).GetIEnumerableGenericType().IsAnonymousType())
                {
                    newFDA.Set(retObject, i + 1, new DALRuntimeTypeList((value as System.Collections.IEnumerable).OfType<Object>().Select(o => o.ToNonAnonymousType() as DALRuntimeTypeBase).ToList()));
                }
                else if (type.IsAnonymousType())
                {
                    newFDA.Set(retObject, i + 1, value.ToNonAnonymousType());
                }
                else
                {
                    newFDA.Set(retObject, i + 1, value);
                }
            }

            return retObject;
        }

        /// <summary>
        /// Gets a runtime typed based on the provided options.
        /// </summary>
        /// <param name="rows">The rows with which to popoulate the runtime types.</param>
        /// <param name="tableName">The table name to set on the runtime type.</param>
        /// <param name="colNames">The column names.</param>
        /// <param name="colTypes">The column types.</param>
        /// <param name="parentChildPropertyName">The parentChildPropertyName list to build into the types.</param>
        /// <returns></returns>
        public static Type GetRuntimeType(this List<Object[]> rows, String tableName, List<String> colNames, List<Type> colTypes, List<String> parentChildPropertyName)
        {
            return CreateType(tableName, rows, colNames, colTypes, parentChildPropertyName);
        }

        /// <summary>
        /// Returns a load opcode for values up to 8.
        /// </summary>
        /// <param name="index">The value.</param>
        /// <returns>The opcode.</returns>
        private static OpCode GetLDC_I4_Code(int index)
        {
            switch (index)
            {
                case 0:
                    return OpCodes.Ldc_I4_0;
                case 1:
                    return OpCodes.Ldc_I4_1;
                case 2:
                    return OpCodes.Ldc_I4_2;
                case 3:
                    return OpCodes.Ldc_I4_3;
                case 4:
                    return OpCodes.Ldc_I4_4;
                case 5:
                    return OpCodes.Ldc_I4_5;
                case 6:
                    return OpCodes.Ldc_I4_6;
                case 7:
                    return OpCodes.Ldc_I4_7;
                default:
                    return OpCodes.Ldc_I4_8;
            }
        }

        /// <summary>
        /// Cache of created types.
        /// </summary>
        private static Dictionary<String, Type> s_createdTypes;

        /// <summary>
        /// Generates property objects for the runtime type.
        /// </summary>
        /// <param name="colNames">The column names.</param>
        /// <param name="colTypes">The column types.</param>
        /// <param name="rows">The data rows representing the objects to populate.</param>
        /// <returns>A dictionary of property names and types.</returns>
        private static Dictionary<String, Type> GetProperties(List<String> colNames, List<Type> colTypes, List<Object[]> rows)
        {
            if (colNames == null || colTypes == null)
                return new Dictionary<String, Type>();

            return colNames.Select((c, i) =>
            {
                Type nullableType;
                if (Extensions.s_NullableTypeLookup.TryGetValue(colTypes[i], out nullableType) && rows.AsParallel().WithDegreeOfParallelism(8).Any(dr => dr[i] == DBNull.Value || dr[i] == null))
                {
                    return new
                    {
                        Name = c,
                        Type = nullableType
                    };
                }
                else
                {
                    return new
                    {
                        Name = c,
                        Type = colTypes[i]
                    };
                }
            }).ToDictionary(c => c.Name, c => c.Type);
        }

        /// <summary>
        /// Wrapper to GenerateType.  Prepares the inputs to be consumed by GenerateType.
        /// </summary>
        /// <param name="tableName">The table name to apply to the created type.</param>
        /// <param name="rows">The data rows which represent the objects to populate.</param>
        /// <param name="colNames">The column names.</param>
        /// <param name="colTypes">The column types.</param>
        /// <param name="parentChildPropertyName">The parentChildPropertyNames to apply to the created type.</param>
        /// <returns>The type.</returns>
        private static Type CreateType(String tableName, List<Object[]> rows, List<String> colNames, List<Type> colTypes, List<String> parentChildPropertyName)
        {
            var properties = GetProperties(colNames, colTypes, rows);
            String typeID = properties.GetUniqueIdentifier(tableName, parentChildPropertyName);
            return GenerateType(tableName, typeID, properties, parentChildPropertyName);
        }

        private static AssemblyBuilder s_ABuilder = Thread.GetDomain().DefineDynamicAssembly(new AssemblyName("DALRuntimeTypes"), AssemblyBuilderAccess.Run);

        /// <summary>
        /// Creates a type based on the provided options.
        /// </summary>
        /// <param name="tableName">The table name of this type.</param>
        /// <param name="typeID">The typeID derived by the call to CreateType.</param>
        /// <param name="properties">The properties derived by the call to CreateType.</param>
        /// <param name="parentChildPropertyName">The parentChildPropertyNames to apply to the created type.</param>
        /// <returns>The type.</returns>
        private static Type GenerateType(String tableName, String typeID, Dictionary<String, Type> properties, List<String> parentChildPropertyName)
        {
            Type type;
            if (s_createdTypes.TryGetValue(typeID, out type))
                return type;
            else
            {
                String asmName = String.Format("Runtime_assembly_{0}", properties.GetHashCode());
                String moduleName = String.Format("{0}_module", asmName);
                String typeName = String.Format("{0}_type", moduleName);

                var module = s_ABuilder.DefineDynamicModule(moduleName);
                //define a new public sealed class which derives from DALRuntimeTypeBase
                //public sealed class typeName : DALRuntimeTypeBase, IQuickPopulate
                var tBuilder = module.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout, typeof(DALRuntimeTypeBase));
                tBuilder.AddInterfaceImplementation(typeof(IQuickPopulate));

                //implement the abstract TableName property
                //public String TableName { get; }
                var tableNameProperty = tBuilder.DefineProperty("TableName", System.Reflection.PropertyAttributes.None, typeof(String), new Type[] { typeof(String) });
                var tableNameAccessorAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual;

                var tableNameGetMethod = tBuilder.DefineMethod("get_TableName", tableNameAccessorAttributes, typeof(String), Type.EmptyTypes);
                var tableNameGetIL = tableNameGetMethod.GetILGenerator();
                tableNameGetIL.Emit(OpCodes.Ldstr, tableName ?? ""); //loads the string stored in tableName
                tableNameGetIL.Emit(OpCodes.Ret); //returns the top of the stack

                tableNameProperty.SetGetMethod(tableNameGetMethod);

                #region Class Properties
                int index = 0;
                var classFields = new List<FieldBuilder>();
                foreach (var p in properties)
                {
                    var propertyName = p.Key;
                    var propertyType = p.Value;

                    var field = tBuilder.DefineField(String.Format("<{0}>k__BackingField", propertyName), propertyType, FieldAttributes.Private);
                    classFields.Add(field);
                    var property = tBuilder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.None, propertyType, new Type[] { propertyType });

                    var accessorAttributes = MethodAttributes.PrivateScope | MethodAttributes.Public | MethodAttributes.HideBySig;

                    var getMethod = tBuilder.DefineMethod("get_" + propertyName, accessorAttributes, propertyType, Type.EmptyTypes);
                    var getIL = getMethod.GetILGenerator();
                    //String myString = myClass.Property;
                    getIL.Emit(OpCodes.Ldarg_0); //loads the class instance myClass onto the stack
                    getIL.Emit(OpCodes.Ldfld, field); //loads the value of the backing field of myClass.Property onto the top of the stack
                    getIL.Emit(OpCodes.Ret); // returns the top of the stack

                    var setMethod = tBuilder.DefineMethod("set_" + propertyName, accessorAttributes, null, new Type[] { propertyType });
                    var setIL = setMethod.GetILGenerator();
                    //myClass.Property = value
                    setIL.Emit(OpCodes.Ldarg_0); //loads the class instance myClass onto the stack 
                    setIL.Emit(OpCodes.Ldarg_1); //loads value onto the stack
                    setIL.Emit(OpCodes.Stfld, field); //sets the value of the backing field of myClass.Property
                    setIL.Emit(OpCodes.Ret); //returns

                    property.SetGetMethod(getMethod);
                    property.SetSetMethod(setMethod);
                    index++;
                }

                //if there will be child objects to store, create an object list as well
                if (parentChildPropertyName != null)
                {
                    foreach (var parent in parentChildPropertyName)
                    {
                        var propertyName = parent;
                        var propertyType = typeof(List<DALRuntimeTypeBase>);

                        var field = tBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);
                        var property = tBuilder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.None, propertyType, new Type[] { propertyType });

                        var accessorAttributes = MethodAttributes.Public | MethodAttributes.HideBySig;

                        var getMethod = tBuilder.DefineMethod("get_" + propertyName, accessorAttributes, propertyType, Type.EmptyTypes);
                        var getIL = getMethod.GetILGenerator();
                        //String myString = myClass.Property;
                        getIL.Emit(OpCodes.Ldarg_0); //loads the class instance myClass onto the stack
                        getIL.Emit(OpCodes.Ldfld, field); //loads the value of the backing field of myClass.Property onto the top of the stack
                        getIL.Emit(OpCodes.Ret); // returns the top of the stack

                        var setMethod = tBuilder.DefineMethod("set_" + propertyName, accessorAttributes, null, new Type[] { propertyType });
                        var setIL = setMethod.GetILGenerator();
                        //myClass.Property = value
                        setIL.Emit(OpCodes.Ldarg_0); //loads the class instance myClass onto the stack 
                        setIL.Emit(OpCodes.Ldarg_1); //loads value onto the stack
                        setIL.Emit(OpCodes.Stfld, field); //sets the value of the backing field of myClass.Property
                        setIL.Emit(OpCodes.Ret); //returns

                        property.SetGetMethod(getMethod);
                        property.SetSetMethod(setMethod);

                        //should be ignored by the DAL for "normal" reading/writing to/from db.  Add the DALIgnoreAttribute to it.
                        var caBuilder = new CustomAttributeBuilder(typeof(DALIgnoreAttribute).GetConstructor(new Type[] { }), new Object[] { });
                        property.SetCustomAttribute(caBuilder);
                    }
                }
                #endregion

                #region IQuickPopulate implementation
                var convertToTMethod = typeof(Extensions).GetMethod("CastToT", new Type[] { typeof(Object) });
                var dalPopulateMethod = tBuilder.DefineMethod("DALPopulate", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual, null, new Type[] { typeof(Object[]), typeof(Dictionary<String, int>) });
                var dalPopulateIL = dalPopulateMethod.GetILGenerator();
                int fieldIndex = 0;
                foreach (var p in properties)
                {
                    /*
                        * public void DALPopulate(Object [] dr, Dictionary<String, int> indexes)
                        * {
                        *      foreach class property
                        *          Property = dr[propertyIndex].ConvertToT<property_type>();
                        * }
                        */
                    dalPopulateIL.Emit(OpCodes.Ldarg_0); //push class instance onto stack
                    dalPopulateIL.Emit(OpCodes.Ldarg_1); //push parameter Object[] dr onto stack
                    //don't have to push Dictionary<String, int> indexes onto the stack because we never use it
                    //load the array index we want to read from the Object[] dr
                    if (fieldIndex <= 8)
                        dalPopulateIL.Emit(GetLDC_I4_Code(fieldIndex));
                    else if (fieldIndex <= 127)
                        dalPopulateIL.Emit(OpCodes.Ldc_I4_S, fieldIndex);
                    else
                        dalPopulateIL.Emit(OpCodes.Ldc_I4, fieldIndex);
                    dalPopulateIL.Emit(OpCodes.Ldelem_Ref); //retrieves the array index from Object[] dr that was loaded onto the stack
                    dalPopulateIL.EmitCall(OpCodes.Call, convertToTMethod.MakeGenericMethod(p.Value), null); //call ConvertToT method.  use MakeGenericMethod to get the generic version of this method which accepts the type we need
                    dalPopulateIL.Emit(OpCodes.Stfld, classFields[fieldIndex++]); //set the field
                }
                dalPopulateIL.Emit(OpCodes.Ret); //return
                #endregion

                type = tBuilder.CreateType();
                lock (s_createdTypes)
                {
                    if (!s_createdTypes.ContainsKey(typeID))
                        s_createdTypes.Add(typeID, type);
                }

                return type;
            }
        }
    }
}