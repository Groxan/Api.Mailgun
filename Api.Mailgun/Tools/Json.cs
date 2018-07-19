using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace Api.Mailgun.Tools
{
    /// <summary>
    /// Helper for serializing json
    /// </summary>
    static class Json
    {
        /// <summary>
        /// Deserialize a json object from the stream
        /// </summary>
        /// <typeparam name="T">Resulting object type</typeparam>
        /// <param name="stream">Stream with json data</param>
        /// <returns>Parsed object</returns>
        public static T ReadObject<T>(Stream stream)
        {
            var settings = new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true };
            var serializer = new DataContractJsonSerializer(typeof(T), settings);
            return (T)serializer.ReadObject(stream);
        }

        /// <summary>
        /// Serialize the dictionary object to json string
        /// </summary>
        /// <param name="obj">Dictionary object to serialize</param>
        /// <returns>Json string</returns>
        public static string Serialize(Dictionary<string, string> obj)
        {
            if (obj == null) return null;

            var settings = new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true };
            var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>), settings);
            using (var mem = new MemoryStream())
            {
                serializer.WriteObject(mem, obj);
                return Encoding.UTF8.GetString(mem.ToArray());
            }
        }
    }
}
