using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Api.Mailgun.Http
{
    /// <summary>
    /// Renewable HttpClient with the specified basic access authentication header
    /// </summary>
    public class RenewableHttpClientAuth : RenewableHttpClient
    {
        /// <summary>
        /// Basic access authentication header
        /// </summary>
        public AuthenticationHeaderValue AuthenticationHeader { get; private set; }

        /// <summary>
        /// Creates an instance of the RenewableHttpClientAuth
        /// </summary>
        /// <param name="user">Basic authentication username</param>
        /// <param name="password">Basic authentication password</param>
        public RenewableHttpClientAuth(string user, string password)
        {
            AuthenticationHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}")));
        }

        protected override HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticationHeader;

            return client;
        }
    }
}
