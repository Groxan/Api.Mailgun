using System;
using System.Net.Http;

namespace Api.Mailgun.Http
{
    /// <summary>
    /// Wrapper for HttpClient with autorenew of the HttpClient due to a DNS udpates issue.
    /// </summary>
    class RenewableHttpClient
    {
        private HttpClient _Client;
        private DateTime _Expiration;

        /// <summary>
        /// Lifetime of the HttpClient in minutes
        /// </summary>
        protected readonly int Lifetime = 60;

        /// <summary>
        /// Actual instance of the HttpClient
        /// </summary>
        public HttpClient Client
        {
            get
            {
                if (_Client == null)
                {
                    _Client = CreateHttpClient();
                    _Expiration = DateTime.UtcNow.AddMinutes(60);
                }
                else if (DateTime.UtcNow > _Expiration)
                {
                    _Client.Dispose();
                    _Client = CreateHttpClient();
                    _Expiration = DateTime.UtcNow.AddMinutes(60);
                }
                return _Client;
            }
        }

        /// <summary>
        /// Creates the HttpClient with the required headers and parameters
        /// </summary>
        /// <returns>Instanse of the HttpClient</returns>
        protected virtual HttpClient CreateHttpClient() => new HttpClient();        
    }
}
