using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Mailgun
{
    /// <summary>
    /// Extensions for reading HttpContent
    /// </summary>
    static class HttpContentExtension
    {
        /// <summary>
        /// Deserialize a json object from the http content
        /// </summary>
        /// <typeparam name="T">Resulting object type</typeparam>
        /// <param name="content">Http content with json data</param>
        /// <returns>Parsed object</returns>
        public static async Task<T> ReadObjectAsync<T>(this HttpContent content)
        {
            return Json.ReadObject<T>(await content.ReadAsStreamAsync());
        }
    }
}
