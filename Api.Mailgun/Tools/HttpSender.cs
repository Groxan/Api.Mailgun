using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

namespace Api.Mailgun
{
    /// <summary>
    /// Wrapper for HttpClient with autorenew of the HttpClient due to a DNS udpates issue
    /// </summary>
    class HttpSender : IDisposable
    {
        /// <summary>
        /// Actual instance of the HttpClient
        /// </summary>
        public HttpClient Client
        {
            get
            {
                lock (this)
                {
                    if (DateTime.UtcNow > Expiration)
                    {
                        var newClient = new HttpClient();
                        newClient.DefaultRequestHeaders.Authorization = _Client.DefaultRequestHeaders.Authorization;

                        _Client.Dispose();
                        _Client = newClient;

                        Expiration = DateTime.UtcNow.AddMinutes(60);
                    }
                }
                return _Client;
            }
        }
        HttpClient _Client;

        DateTime Expiration;

        /// <summary>
        /// Creates an instance of the HttpSender
        /// </summary>
        /// <param name="user">Basic authentication username</param>
        /// <param name="password">Basic authentication password</param>
        public HttpSender(string user, string password)
        {
            _Client = new HttpClient();
            _Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}")));

            Expiration = DateTime.UtcNow.AddMinutes(60);
        }

        /// <summary>
        /// Sends request to Mailgun
        /// </summary>
        /// <typeparam name="T">Type of the expected response</typeparam>
        /// <param name="request">Http request message</param>
        /// <returns></returns>
        public async Task<Result<T>> SendRequest<T>(HttpRequest<T> request)
        {
            try
            {
                var response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return Result<T>.Fail((int)response.StatusCode);

                return Result<T>.Success(await response.Content.ReadObjectAsync<T>());
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
