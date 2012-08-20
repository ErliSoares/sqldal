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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Threading;
using System.Data.DBAccess.Generic.Exceptions;

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Class which provides fast reflection type access to a class at runtime.
    /// </summary>
    public sealed class FastDynamicAccess
    {
        #region Properties
        /// <summary>
        /// Dictionary of property names to the methods to get/set that property.
        /// </summary>
        private Dictionary<String, IFastDynamicAccess> m_accessors;

        /// <summary>
        /// Dictionary which maps CLR types to the opcode used to load an object of that type.
        /// </summary>
        private Dictionary<Type, OpCode> m_loadOpCodes = new Dictionary<Type, OpCode>
        {
            { typeof(sbyte), OpCodes.Ldind_I1 },
            { typeof(byte), OpCodes.Ldind_U1 },
            { typeof(char), OpCodes.Ldind_U2 },
            { typeof(short), OpCodes.Ldind_I2 },
            { typeof(ushort), OpCodes.Ldind_U2 },
            { typeof(int), OpCodes.Ldind_I4 },
            { typeof(uint), OpCodes.Ldind_U4 },
            { typeof(long), OpCodes.Ldind_I8 },
            { typeof(ulong), OpCodes.Ldind_I8 },
            { typeof(bool), OpCodes.Ldind_I1 },
            { typeof(double), OpCodes.Ldind_R8 },
            { typeof(float), OpCodes.Ldind_R4 },
        };

        private IFastDynamicAccess[] m_AccessorsArray;
        public Dictionary<String, int> PropertyToArrayIndex { get; private set; }
        #endregion

        #region static functionality
        /// <summary>
        /// Cache of types and their FastDynamicAccess object.
        /// </summary>
        private static Dictionary<Type, FastDynamicAccess> m_fdas;

        /// <summary>
        /// Static constructor.  Instantiates the _fdas dictionary.
        /// </summary>
        static FastDynamicAccess()
        {
            m_fdas = new Dictionary<Type, FastDynamicAccess>();
        }

        /// <summary>
        /// Gets a FastDynamicAccess object for the type represented by the provided object.
        /// </summary>
        /// <param name="obj">The object whose type should be used to create the FastDynamicAccess object.</param>
        /// <returns>The FastDynamicAccess object.</returns>
        public static FastDynamicAccess Get(Object obj)
        {
            return FastDynamicAccess.Get(obj.GetType());
        }

        /// <summary>
        /// Gets a FastDynamicAccess object for the provided type.
        /// </summary>
        /// <param name="type">The type to use to create the FastDynamicAcess object.</param>
        /// <returns>The FastDynamicAccess object.</returns>
        public static FastDynamicAccess Get(Type type)
        {
            FastDynamicAccess fda;
            lock (m_fdas)
            {
                if (!FastDynamicAccess.m_fdas.TryGetValue(type, out fda))
                {
                    fda = new FastDynamicAccess(type);
                    FastDynamicAccess.m_fdas.Add(type, fda);
                }
            }

            return fda;
        }
        #endregion

        /// <summary>
        /// Instantiates a new FastDynamicAccess object using the provided type.  Private.  To get a FDA object use one of the static Get methods.
        /// </summary>
        /// <param name="type">The type to use to create the FastDynamicAccess object.</param>
        private FastDynamicAccess(Type type)
        {
            var ps = type.GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic).ToList();
            this.m_accessors = new Dictionary<String, IFastDynamicAccess>();
            this.m_AccessorsArray = new IFastDynamicAccess[ps.Count];
            this.PropertyToArrayIndex = new Dictionary<String, int>();

            this.GenerateAssemblies(type, ps);
        }

        /// <summary>
        /// Generates assembly and getter/setter methods for each property in a type.
        /// </summary>
        /// <param name="type">The type.</param>
        private void GenerateAssemblies(Type type, List<PropertyInfo> properties)
        {
            int index = 0;
            foreach (var p in properties)
            {
                var aName = new AssemblyName();
                aName.Name = "FastDynamicAccessAccessors";

                var aBuilder = Thread.GetDomain().DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
                var module = aBuilder.DefineDynamicModule("Module");

                String className = String.Format("{0}.{1}_Accessors", type.Namespace + type.Name, p.Name);
                var tBuilder = module.DefineType(className, TypeAttributes.Public);

                tBuilder.AddInterfaceImplementation(typeof(IFastDynamicAccess));

                var constructor = tBuilder.DefineDefaultConstructor(MethodAttributes.Public);


                this.DefineGetMethod(type, tBuilder, p.Name);
                this.DefineSetMethod(type, tBuilder, p.Name);

                var t = tBuilder.CreateType();

                var o = FormatterServices.GetUninitializedObject(t);

                this.m_accessors.Add(p.Name, o as IFastDynamicAccess);
                this.m_AccessorsArray[index] = o as IFastDynamicAccess;
                this.PropertyToArrayIndex.Add(p.Name, index);
                index++;
            }
        }

        /// <summary>
        /// Defines a get method by emitting opcodes.
        /// </summary>
        /// <param name="type">The type represented by the FDA.</param>
        /// <param name="typeBuilder">The type builder for the model property.</param>
        /// <param name="propertyName">The property name.</param>
        private void DefineGetMethod(Type type, TypeBuilder typeBuilder, String propertyName)
        {
            var m = type.GetMethod("get_" + propertyName, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic);

            var method = typeBuilder.DefineMethod(
                "Get",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(Object),
                new Type[] { typeof(Object) });

            var il = method.GetILGenerator();

            if (m != null)
            {
                il.DeclareLocal(typeof(Object));
                //Load the first argument (source object)
                il.Emit(OpCodes.Ldarg_1);

                //Cast to the source type
                il.Emit(OpCodes.Castclass, type);

                //Get the property value
                //this will place the returned value at the top of the stack
                il.EmitCall(OpCodes.Call, m, null);

                //Box if necessary
                if (m.ReturnType.IsValueType)
                {
                    il.Emit(OpCodes.Box, m.ReturnType);
                }
            }

            il.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Defines a set method by emitting opcodes.
        /// </summary>
        /// <param name="type">The type represented by the FDA.</param>
        /// <param name="typeBuilder">The type builder for the model property.</param>
        /// <param name="propertyName">The property name.</param>
        private void DefineSetMethod(Type type, TypeBuilder typeBuilder, String propertyName)
        {
            var m = type.GetMethod("set_" + propertyName, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            var propertyType = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic).PropertyType;

            var method = typeBuilder.DefineMethod(
                "Set",
                MethodAttributes.Public | MethodAttributes.Virtual,
                null,
                new Type[] { typeof(Object), typeof(Object) });

            var il = method.GetILGenerator();

            if (m != null)
            {
                //load first argument (target object)
                il.Emit(OpCodes.Ldarg_1);

                //cast to targetType
                il.Emit(OpCodes.Castclass, type);

                //load the second argument (object value)
                il.Emit(OpCodes.Ldarg_2);

                if (propertyType.IsValueType)
                {
                    //unbox it
                    il.Emit(OpCodes.Unbox, propertyType);

                    //load what was unboxed to top of stack
                    OpCode loadCode;
                    if (this.m_loadOpCodes.TryGetValue(propertyType, out loadCode))
                    {
                        il.Emit(loadCode);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldobj, propertyType);
                    }
                }
                else
                {
                    il.Emit(OpCodes.Castclass, propertyType);
                }

                //call the set_ method
                il.EmitCall(OpCodes.Callvirt, m, null);
            }

            il.Emit(OpCodes.Ret);
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

        private static Dictionary<String, GetModelPopulateMethodDelegate> s_ModelPopulateCache = new Dictionary<String, GetModelPopulateMethodDelegate>();
        internal static GetModelPopulateMethodDelegate GetModelPopulateMethod(List<String> propertyNames, List<String> stringFormats, List<Type> propertyTypes, Type modelType)
        {
            var methodName = String.Format("Populate_{0}", (String.Join("", propertyNames.Select(p => p ?? "nullProperty")) + modelType.Assembly.FullName + modelType.FullName).GenerateHash().Replace("-", ""));
            GetModelPopulateMethodDelegate gmpmd;

            if (!s_ModelPopulateCache.TryGetValue(methodName, out gmpmd))
            {
                var sfMeth = typeof(String).GetMethod("Format", new Type[] { typeof(String), typeof(Object) });
                var meth = new DynamicMethod(methodName, typeof(void), new Type[] { modelType, typeof(Object), typeof(Object[]) }, true);
                var il = meth.GetILGenerator();

                //il.BeginExceptionBlock();
                il.DeclareLocal(modelType); // stores model reference
                il.DeclareLocal(typeof(Object)); // stores object from dr[]

                il.Emit(OpCodes.Ldarg_1); //push class instance onto stack
                il.Emit(OpCodes.Castclass, modelType); //cast it to the model type since it's an object
                il.Emit(OpCodes.Stloc_0); //store it into the local variable

                /*
                 * for (int i = 0; i < dr.Length; i++)
                 * {
                 *      if (model.Property[i].IsValueType)
                 *      {
                 *          model.Property[i] = (T)dr[i];
                 *      }
                 *      else
                 *      {
                 *          if (dr[i] == DBNull.Value)
                 *              model.Property[i] = null;
                 *          else
                 *              model.Property[i] = (T)dr[i];
                 *      }
                 * }
                 */

                for (int i = 0; i < propertyNames.Count; i++)
                {
                    //no mapping from datarow to model, ignore it
                    if (propertyNames[i] == null)
                        continue;

                    var setMethod = modelType.GetMethod("set_" + propertyNames[i]);
                    if (setMethod == null)
                        continue;

                    il.BeginExceptionBlock();
                    il.Emit(OpCodes.Ldloc_0);
                    if (stringFormats[i] != null)
                    {
                        il.Emit(OpCodes.Ldstr, stringFormats[i]); //load string format string onto stack if needed
                    }
                    il.Emit(OpCodes.Ldarg_2); //push parameter Object[] dr onto stack

                    //load the array index we want to read from the Object[] dr
                    if (i <= 8)
                        il.Emit(GetLDC_I4_Code(i));
                    else
                        il.Emit(OpCodes.Ldc_I4_S, i);

                    il.Emit(OpCodes.Ldelem_Ref); //retrieves the array index from Object[] dr that was loaded onto the stack

                    var pType = propertyTypes[i];
                    if (!pType.IsValueType || pType.IsNullableValueType())
                    {
                        //if it's a ref type we'll want to store it for later use
                        il.Emit(OpCodes.Stloc_1); //store it for later use
                        il.Emit(OpCodes.Ldloc_1);
                    }

                    if (stringFormats[i] != null)
                    {
                        il.Emit(OpCodes.Call, sfMeth); //call the string format method if needed
                        //if string format then we can skip right to the bottom
                    }
                    else if (!pType.IsValueType || pType.IsNullableValueType()) // ref type
                    {
                        /*if (value == DBNull.Value)
                         *      value = null;
                         * 
                         * return (T)value;
                         */
                        var setPropertyLabel = il.DefineLabel();
                        var loadNullLabel = il.DefineLabel();

                        il.Emit(OpCodes.Ldsfld, typeof(DBNull).GetField("Value"));
                        il.Emit(OpCodes.Beq, loadNullLabel); //if (value == DBNull.Value) jump to load null

                        il.Emit(OpCodes.Ldloc_1); //otherwise get the value back on the top of the stack
                        il.Emit(OpCodes.Br, setPropertyLabel);

                        //load null onto stack and jump to set the method
                        il.MarkLabel(loadNullLabel);
                        il.Emit(OpCodes.Ldnull); // load a null

                        il.MarkLabel(setPropertyLabel);
                        if (pType.IsValueType)
                        {
                            il.Emit(OpCodes.Unbox, pType);
                            il.Emit(OpCodes.Ldobj, pType);
                        }
                        else
                        {
                            il.Emit(OpCodes.Castclass, pType);
                        }
                    }
                    else // value type
                    {
                        //simply cast to the value type
                        il.Emit(OpCodes.Castclass, pType);
                        il.Emit(OpCodes.Unbox_Any, pType); //unbox if a value type
                    }

                    il.Emit(OpCodes.Callvirt, setMethod); //set the property

                    il.BeginCatchBlock(typeof(InvalidCastException));
                    il.Emit(OpCodes.Pop); //exception is first on the stack
                    //load exception message.. only variable we don't know at method creation time is the type of the value that failed
                    il.Emit(OpCodes.Ldstr, String.Format("Object passed with type '{{0}}' cannot be assigned to the type '{0}' (model '{1}' property '{2}')", propertyTypes[i], modelType, propertyNames[i]));

                    //load the value that we are trying to set... if it was a value type we never stored it in loc1 to increase speed for value types.
                    if (!pType.IsValueType)
                    {
                        il.Emit(OpCodes.Ldloc_1); // the object from dr[]
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldarg_2); //push parameter Object[] dr onto stack

                        //load the array index we want to read from the Object[] dr
                        if (i <= 8)
                            il.Emit(GetLDC_I4_Code(i));
                        else
                            il.Emit(OpCodes.Ldc_I4_S, i);

                        il.Emit(OpCodes.Ldelem_Ref); //retrieves the array index from Object[] dr that was loaded onto the stack
                    }

                    il.Emit(OpCodes.Callvirt, typeof(Object).GetMethod("GetType"));
                    il.Emit(OpCodes.Call, sfMeth);
                    il.Emit(OpCodes.Newobj, typeof(ModelPropertyColumnMismatchException).GetConstructor(new Type[] { typeof(String) }));
                    il.Emit(OpCodes.Throw);
                    il.EndExceptionBlock();
                }

                il.Emit(OpCodes.Ret);

                gmpmd = (GetModelPopulateMethodDelegate)meth.CreateDelegate(typeof(GetModelPopulateMethodDelegate));
                s_ModelPopulateCache.Add(methodName, gmpmd);
            }

            return gmpmd;
        }

        internal delegate void GetModelPopulateMethodDelegate(Object model, Object[] dr);

        /// <summary>
        /// Gets the specified property of the provided object.
        /// </summary>
        /// <param name="source">The object from which to read.</param>
        /// <param name="propertyName">The property to read.</param>
        /// <returns>The value of the property.</returns>
        public Object Get(Object source, String propertyName)
        {
            return this.m_accessors[propertyName].Get(source);
        }

        /// <summary>
        /// Gets the specified property of the provided object.
        /// </summary>
        /// <param name="source">The object from which to read.</param>
        /// <param name="index">The index of the property.</param>
        /// <returns>The value of the property.</returns>
        public Object Get(Object source, int index)
        {
            return this.m_AccessorsArray[index].Get(source);
        }

        /// <summary>
        /// Gets the specified property of the provided object.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="source">The object from which to read.</param>
        /// <param name="propertyName">The property to read.</param>
        /// <returns>The value of the property.</returns>
        public T Get<T>(Object source, String propertyName)
        {
            return (T)this.Get(source, propertyName);
        }

        /// <summary>
        /// Gets the specified property of the provided object.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="source">The object from which to read.</param>
        /// <param name="index">The index of the property.</param>
        /// <returns>The value of the property.</returns>
        public T Get<T>(Object source, int index)
        {
            return (T)this.Get(source, index);
        }

        /// <summary>
        /// Used internally by IDBAccess.  This is a custom function to get a property from a model as a list.  This is used when relating object lists together after an ExecuteRelatedSetRead.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The list of objects.</returns>
        internal List<Object> GetList(Object source, String propertyName)
        {
            var list = this.Get<IEnumerable>(source, propertyName);
            if (list == null)
                return null;

            return list.OfType<Object>().ToList();
        }

        /// <summary>
        /// Sets the value of a specified parameter of an object.
        /// </summary>
        /// <param name="target">The object to which to write.</param>
        /// <param name="propertyName">The property to write.</param>
        /// <param name="value">The value to write.</param>
        public void Set(Object target, String propertyName, Object value)
        {
            this.m_accessors[propertyName].Set(target, value);
        }

        /// <summary>
        /// Sets the value of a specified parameter of an object.
        /// </summary>
        /// <param name="target">The object to which to write.</param>
        /// <param name="index">The index of the property.</param>
        /// <param name="value">The value to write.</param>
        public void Set(Object target, int index, Object value)
        {
            this.m_AccessorsArray[index].Set(target, value);
        }
    }
}