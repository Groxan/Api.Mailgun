using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

using Api.Mailgun.Http;
using Api.Mailgun.Requests;

namespace Api.Mailgun
{
    /// <summary>
    /// Wrapper for the Mailgun API
    /// </summary>
    public class Mailgun
    {
        readonly string BaseUri;
        readonly string WorkDomain;
        readonly RenewableHttpClientAuth Http;

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
            Http = new RenewableHttpClientAuth("api", apiKey);            
        }

        #region Mailing lists
        /// <summary>
        /// Creates a new mailing list
        /// </summary>
        /// <param name="alias">Alias name of the mailing list. The result address will looks like 'alias@WorkDomain'.</param>
        /// <returns></returns>
        public async Task<Result<CreateMailingListResponse>> CreateMailingListAsync(string alias)
        {
            #region check args
            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException(nameof(alias));
            #endregion

            return await SendRequest<CreateMailingListResponse>(new CreateMailingListRequest(BaseUri, WorkDomain, alias, null, null, AccessLevels.Readonly));
        }

        /// <summary>
        /// Creates a new mailing list
        /// </summary>
        /// <param name="alias">Alias name of the mailing list. The result address will looks like 'alias@WorkDomain'.</param>
        /// <param name="name">Display name of the mailing list </param>
        /// <returns></returns>
        public async Task<Result<CreateMailingListResponse>> CreateMailingListAsync(string alias, string name)
        {
            #region check args
            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException(nameof(alias));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            #endregion

            return await SendRequest<CreateMailingListResponse>(new CreateMailingListRequest(BaseUri, WorkDomain, alias, name, null, AccessLevels.Readonly));
        }

        /// <summary>
        /// Creates a new mailing list
        /// </summary>
        /// <param name="alias">Alias name of the mailing list. The result address will looks like 'alias@WorkDomain'.</param>
        /// <param name="name">Display name of the mailing list </param>
        /// <param name="description">Mailing list description</param>
        /// <returns></returns>
        public async Task<Result<CreateMailingListResponse>> CreateMailingListAsync(string alias, string name, string description)
        {
            #region check args
            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException(nameof(alias));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));
            #endregion

            return await SendRequest<CreateMailingListResponse>(new CreateMailingListRequest(BaseUri, WorkDomain, alias, name, description, AccessLevels.Readonly));
        }

        /// <summary>
        /// Creates a new mailing list
        /// </summary>
        /// <param name="alias">Alias name of the mailing list. The result address will looks like 'alias@WorkDomain'.</param>
        /// <param name="name">Display name of the mailing list</param>
        /// <param name="description">Mailing list description</param>
        /// <param name="access">Mailing list access level</param>
        /// <returns></returns>
        public async Task<Result<CreateMailingListResponse>> CreateMailingListAsync(string alias, string name, string description, AccessLevels access)
        {
            #region check args
            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException(nameof(alias));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));
            #endregion

            return await SendRequest<CreateMailingListResponse>(new CreateMailingListRequest(BaseUri, WorkDomain, alias, name, description, access));
        }

        /// <summary>
        /// Update mailing list alias name
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="alias">New alias name. The result address will looks like 'alias@WorkDomain'.</param>
        /// <returns></returns>
        public async Task<Result<UpdateMailingListResponse>> UpdateMailingListAliasAsync(string list, string alias)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException(nameof(alias));
            #endregion

            return await SendRequest<UpdateMailingListResponse>(new UpdateMailingListRequest(BaseUri, WorkDomain, list, alias, null, null, null));
        }

        /// <summary>
        /// Update mailing list display name
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="name">New mailing list display name</param>
        /// <returns></returns>
        public async Task<Result<UpdateMailingListResponse>> UpdateMailingListNameAsync(string list, string name)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            #endregion

            return await SendRequest<UpdateMailingListResponse>(new UpdateMailingListRequest(BaseUri, WorkDomain, list, null, name, null, null));
        }

        /// <summary>
        /// Updates mailing list description
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="description">New mailing list description</param>
        /// <returns></returns>
        public async Task<Result<UpdateMailingListResponse>> UpdateMailingListDescriptionAsync(string list, string description)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));
            #endregion

            return await SendRequest<UpdateMailingListResponse>(new UpdateMailingListRequest(BaseUri, WorkDomain, list, null, null, description, null));
        }

        /// <summary>
        /// Updates mailing list access
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="access">New mailing list access level</param>
        /// <returns></returns>
        public async Task<Result<UpdateMailingListResponse>> UpdateMailingListAccessAsync(string list, AccessLevels access)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));
            #endregion

            return await SendRequest<UpdateMailingListResponse>(new UpdateMailingListRequest(BaseUri, WorkDomain, list, null, null, null, access));
        }
        
        /// <summary>
        /// Deletes a mailing list
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <returns></returns>
        public async Task<Result<DeleteMailingListResponse>> DeleteMailingListAsync(string list)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));
            #endregion

            return await SendRequest<DeleteMailingListResponse>(new DeleteMailingListRequest(BaseUri, WorkDomain, list));
        }
        #endregion

        #region Mailing lists members
        /// <summary>
        /// Adds a member to the mailing list
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="email">Email address of the member</param>
        /// <returns></returns>
        public async Task<Result<AddMemberResponse>> AddMemberToListAsync(string list, string email)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));
            #endregion

            return await SendRequest<AddMemberResponse>(new AddMemberRequest(BaseUri, WorkDomain, list, email, null, null, true, true));
        }

        /// <summary>
        /// Adds a member to the mailing list
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="email">Email address of the member</param>
        /// <param name="name">Optional member name</param>
        /// <returns></returns>
        public async Task<Result<AddMemberResponse>> AddMemberToListAsync(string list, string email, string name)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            #endregion

            return await SendRequest<AddMemberResponse>(new AddMemberRequest(BaseUri, WorkDomain, list, email, name, null, true, true));
        }

        /// <summary>
        /// Adds a member to the mailing list
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="email">Email address of the member</param>
        /// <param name="name">Optional member name</param>
        /// <param name="vars">Additional parameters, e.g. {"gender", "female"}, {"age", "27"}</param>
        /// <returns></returns>
        public async Task<Result<AddMemberResponse>> AddMemberToListAsync(string list, string email, string name, Dictionary<string, string> vars)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (vars == null)
                throw new ArgumentNullException(nameof(vars));
            #endregion

            return await SendRequest<AddMemberResponse>(new AddMemberRequest(BaseUri, WorkDomain, list, email, name, vars, true, true));
        }

        /// <summary>
        /// Updates a mailing list member with given properties
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="member">Email address of the member</param>
        /// <param name="email">New email address of the memeber</param>
        /// <returns></returns>
        public async Task<Result<UpdateMemberResponse>> UpdateMemberAddressAsync(string list, string member, string email)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(member))
                throw new ArgumentNullException(nameof(member));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));
            #endregion

            return await SendRequest<UpdateMemberResponse>(new UpdateMemberRequest(BaseUri, WorkDomain, list, member, email, null, null, null));
        }

        /// <summary>
        /// Updates a mailing list member with given properties
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="member">Email address of the member</param>
        /// <param name="name">Optional member name</param>
        /// <returns></returns>
        public async Task<Result<UpdateMemberResponse>> UpdateMemberNameAsync(string list, string member, string name)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(member))
                throw new ArgumentNullException(nameof(member));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            #endregion

            return await SendRequest<UpdateMemberResponse>(new UpdateMemberRequest(BaseUri, WorkDomain, list, member, null, name, null, null));
        }

        /// <summary>
        /// Updates a mailing list member with given properties
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="member">Email address of the member</param>
        /// <param name="name">Optional member name</param>
        /// <returns></returns>
        public async Task<Result<UpdateMemberResponse>> UpdateMemberVarsAsync(string list, string member, Dictionary<string, string> vars)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(member))
                throw new ArgumentNullException(nameof(member));

            if (vars == null)
                throw new ArgumentNullException(nameof(vars));
            #endregion

            return await SendRequest<UpdateMemberResponse>(new UpdateMemberRequest(BaseUri, WorkDomain, list, member, null, null, vars, null));
        }

        /// <summary>
        /// Updates a mailing list member with given properties
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="member">Email address of the member</param>
        /// <param name="subscribed">Status of the member subscription</param>
        /// <returns></returns>
        public async Task<Result<UpdateMemberResponse>> UpdateMemberStatusAsync(string list, string member, bool subscribed)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(member))
                throw new ArgumentNullException(nameof(member));
            #endregion

            return await SendRequest<UpdateMemberResponse>(new UpdateMemberRequest(BaseUri, WorkDomain, list, member, null, null, null, subscribed));
        }

        /// <summary>
        /// Deletes a mailing list member
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="member">Email address of the member</param>
        /// <returns></returns>
        public async Task<Result<RemoveMemberResponse>> RemoveMemberFromListAsync(string list, string member)
        {
            #region check args
            if (string.IsNullOrEmpty(list))
                throw new ArgumentNullException(nameof(list));

            if (string.IsNullOrEmpty(member))
                throw new ArgumentNullException(nameof(member));
            #endregion

            return await SendRequest<RemoveMemberResponse>(new RemoveMemberRequest(BaseUri, WorkDomain, list, member));
        }
        #endregion

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

            return await SendRequest<SendMessageResponse>(new SendMessageRequest(BaseUri, WorkDomain, message));
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

        private async Task<Result<T>> SendRequest<T>(HttpRequestMessage request)
        {
            try
            {
                var response = await Http.Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return Result<T>.Fail((int)response.StatusCode);

                return Result<T>.Success(await response.Content.ReadObjectAsync<T>());
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(ex.Message);
            }
        }
    }
}
