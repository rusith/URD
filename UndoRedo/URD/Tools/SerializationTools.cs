using System;
using System.IO;
using System.Xml.Serialization;

namespace URD.Tools
{
    public static class SerializationTools
    {
        #region Serialization Methods
        /// <summary>
        /// serialize an object 
        /// </summary>
        /// <param name="obj">object to serialize</param>
        /// <returns>returns serialized xml string</returns>
        public static string SerializeObject(object obj)
        {
            XmlSerializer xmlserializer = new XmlSerializer(obj.GetType());
            using (StringWriter writer = new StringWriter())
            {
                xmlserializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        /// <summary>
        /// De serialize an string
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str">string to de serialize</param>
        /// <returns>returns De serialized object</returns>
        public static object DeserializeString(Type type, string str)
        {
            var xmlserializer = new XmlSerializer(type);
            using (TextReader reader = new StringReader(str))
            {
                return  xmlserializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// get deep copy of a object using serialization method [serialize-->de serialize-->return]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>coyed object</returns>
        public static object GetCopyOfAObject(object obj)
        {
            return obj == null ? null : DeserializeString(obj.GetType(), SerializeObject(obj));
        }

        #endregion
    }
}
