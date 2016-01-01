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
        /// <param name="encrypt">true=return text will encrypted, false=returns normal text</param>
        /// <returns></returns>
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
        /// deserialize an string
        /// </summary>
        /// <param name="typeofobject">type of the object</param>
        /// <param name="str">string to deserialize</param>
        /// <param name="decrypt">is string encrypted</param>
        /// <returns></returns>
        public static object DeserializeString(Type type, string str)
        {

            XmlSerializer xmlserializer = new XmlSerializer(type);
            using (TextReader reader = new StringReader(str))
            {
                object deserialized = xmlserializer.Deserialize(reader);

                return deserialized;
            }
        }

        public static object GetCopyOfAObject(object obj)
        {
            if (obj == null) return null;
            return DeserializeString(obj.GetType(), SerializeObject(obj));
        }

        #endregion
    }
}
