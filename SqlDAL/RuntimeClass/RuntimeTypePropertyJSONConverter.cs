using System.Collections.Generic;
using System.Data.DBAccess.Generic.RuntimeClass;
using System.Linq;
using System.Web.Script.Serialization;

namespace System.Data.DBAccess.Generic
{
    /// <summary>
    /// Custom JSON serializer.  Allows the TableName property to be included or excluded for DALRuntimeTypeBase objects.
    /// </summary>
    internal class RuntimeTypePropertyJSONConverter : JavaScriptConverter
    {
        /// <summary>
        /// Boolean whether or not to include the TableName property.
        /// </summary>
        internal Boolean IncludeTableName { get; set; }

        /// <summary>
        /// Default constructor.  IncludeTableName will be false.
        /// </summary>
        internal RuntimeTypePropertyJSONConverter() : this(false) { }

        /// <summary>
        /// Constructor accepting a true/false for the IncludeTableName setting.
        /// </summary>
        /// <param name="includeTableName">True/False to include the TableName property or not.</param>
        internal RuntimeTypePropertyJSONConverter(Boolean includeTableName)
        {
            this.IncludeTableName = includeTableName;
        }

        /// <summary>
        /// Deserialize is not implemented.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="type"></param>
        /// <param name="serializer"></param>
        /// <returns>Throws NotImplementedException.</returns>
        public override object Deserialize(IDictionary<String, Object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a dictionary of property names and values for the serializer.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="serializer">The serializer.  Unused.</param>
        /// <returns>The dictionary.</returns>
        public override IDictionary<String, Object> Serialize(Object obj, JavaScriptSerializer serializer)
        {
            Type type = obj.GetType();
            var fda = FastDynamicAccess.Get(type);
            int i = 0;
            var properties = type.GetProperties();
            //if not including the name and this is a runtimebase, don't want to exclude a TableName property from a different class!
            if (!this.IncludeTableName && obj is DALRuntimeTypeBase)
            {
                //filter out TableName property.
                properties = properties.Where(p => p.Name != "TableName").ToArray();

                //default the index to be 1
                i++;
            }

            if (obj is SerializableLINQGrouping)
            {
                return new Dictionary<String, Object> { { "", obj.GetValue<List<Object>>("Groups").Select(g => this.Serialize(g, serializer)).ToList() } };
            }
            else
            {
                return properties.ToDictionary(p => p.Name, p =>
                {
                    var retValue = fda.Get(obj, i++);
                    if (retValue is List<DALRuntimeTypeBase>)
                        return SerializeListDALRuntimeTypeBase(retValue, serializer);
                    else
                        return retValue;
                });
            }
        }

        /// <summary>
        /// Serializer helper for a List of DALRuntimeTypeBase objects.
        /// </summary>
        /// <param name="obj">The DALRuntimeTypeBase list to serialize.</param>
        /// <param name="serializer">The JavaScriptSerializer object.</param>
        /// <returns>The serialized list.</returns>
        private Object SerializeListDALRuntimeTypeBase(Object obj, JavaScriptSerializer serializer)
        {
            var list = obj as List<DALRuntimeTypeBase>;
            return list.Select(o =>
                this.Serialize(o, serializer)).ToList() as Object;
        }

        /// <summary>
        /// The supported types for which this custom serializer will run.
        /// </summary>
        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                yield return typeof(SerializableLINQGrouping);
                yield return typeof(DALRuntimeTypeBase);
                yield return typeof(List<DALRuntimeTypeBase>);
            }
        }
    }
}