using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Api.Mailgun.MailingLists;
using Api.Mailgun.Routes;

namespace Api.Mailgun
{
    /// <summary>
    /// Wrapper for the Mailgun API
    /// </summary>
    public class Mailgun : IDisposable
    {
        readonly string BaseUri;
        readonly string WorkDomain;
        readonly HttpSender Http;
        
        /// <summary>
        /// Provides access to the Mailing lists API
        /// </summary>
        public MailingListManager Lists
        {
            get
            {
                if (_Lists == null)
                    _Lists = new MailingListManager(WorkDomain, BaseUri, Http);

                return _Lists;
            }
        }
        MailingListManager _Lists;

        /// <summary>
        /// Provides access to the Routes API
        /// </summary>
        public RouteManager Routes
        {
            get
            {
                if (_Routes == null)
                    _Routes = new RouteManager(BaseUri, Http);
                
                return _Routes;
            }
        }
        RouteManager _Routes;

        /// <summary>
        /// Creates an instanse of the Mailgun
        /// </summary>
        /// <param name="workDomain">Your sending domain</param>
        /// <param name="apiKey">Your api key</param>
        /// <param name="baseUri">Base uri for api requests</param>
        public Mailgun(string workDomain, string apiKey, string baseUri = "https://api.mailgun.net/v3")
        {
            #region check args
            if (string.IsNullOrEmpty(workDomain))
                throw new ArgumentNullException(nameof(workDomain));

            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            if (string.IsNullOrEmpty(baseUri))
                throw new ArgumentNullException(nameof(baseUri));
            #endregion

            BaseUri = baseUri;
            WorkDomain = workDomain;
            Http = new HttpSender("api", apiKey);            
        }

        #region Messaging
        /// <summary>
        /// Sends a message by assembling it from the components
        /// </summary>
        /// <param name="message">Mailgun message</param>
        /// <returns></returns>
        public async Task<Result<SendMessageResponse>> SendMessageAsync(Message message)
        {
            #region check args
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            message.EnsureValid();
            #endregion

            return await Http.SendRequest(new SendMessageRequest(BaseUri, WorkDomain, message));
        }

        /// <summary>
        /// Sends a simple message with HTML body
        /// </summary>
        /// <param name="from">Email address of the sender</param>
        /// <param name="to">Email address of the recipient</param>
        /// <param name="subject">Subject of the message</param>
        /// <param name="html">HTML body of the message</param>
        /// <param name="requireTls">Requires the message only be sent over a TLS connection</param>
        /// <returns></returns>
        public async Task<Result<SendMessageResponse>> SendMessageAsync(EmailAddress from, EmailAddress to, string subject, string html, bool requireTls = false)
        {
            #region check args
            if (from == null)
                throw new ArgumentNullException(nameof(from));

            if (to == null)
                throw new ArgumentNullException(nameof(to));

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException(nameof(subject));

            if (string.IsNullOrEmpty(html))
                throw new ArgumentNullException(nameof(html));
            #endregion

            var message = new Message()
            {
                From = from,
                To = new List<EmailAddress>() { to },
                Subject = subject,
                Html = html,
                Dkim = true,
                RequireTls = requireTls
            };

            return await SendMessageAsync(message);
        }

        /// <summary>
        /// Sends a simple message with HTML body
        /// </summary>
        /// <param name="from">Email address of the sender</param>
        /// <param name="to">Email address of the recipient</param>
        /// <param name="subject">Subject of the message</param>
        /// <param name="html">HTML body of the message</param>
        /// <param name="requireTls">Requires the message only be sent over a TLS connection</param>
        /// <returns></returns>
        public async Task<Result<SendMessageResponse>> SendMessageAsync(EmailAddress from, string to, string subject, string html, bool requireTls = false)
        {
            #region check args
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException(nameof(to));
            #endregion

            return await SendMessageAsync(from, new EmailAddress(to), subject, html, requireTls);
        }

        /// <summary>
        /// Sends a simple message with HTML body
        /// </summary>
        /// <param name="from">Email address of the sender</param>
        /// <param name="to">Email address of the recipient</param>
        /// <param name="subject">Subject of the message</param>
        /// <param name="html">HTML body of the message</param>
        /// <param name="requireTls">Requires the message only be sent over a TLS connection</param>
        /// <returns></returns>
        public async Task<Result<SendMessageResponse>> SendMessageAsync(string from, string to, string subject, string html, bool requireTls = false)
        {
            #region check args
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException(nameof(to));

            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException(nameof(to));
            #endregion

            return await SendMessageAsync(new EmailAddress(from), new EmailAddress(to), subject, html, requireTls);
        }

        /// <summary>
        /// Sends a simple message with HTML body to mailing list
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="from">Email address of the sender</param>
        /// <param name="subject">Subject of the message</param>
        /// <param name="html">HTML body of the message</param>
        /// <param name="requireTls">Requires the message only be sent over a TLS connection</param>
        /// <returns></returns>
        public async Task<Result<SendMessageResponse>> SendMessageToListAsync(string list, EmailAddress from, string subject, string html, bool requireTls = false)
        {
            return await SendMessageAsync(from, new EmailAddress($"{list}@{WorkDomain}"), subject, html, requireTls);
        }
        #endregion

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources
        /// </summary>
        public void Dispose()
        {
            Http.Dispose();
        }
    }
}
