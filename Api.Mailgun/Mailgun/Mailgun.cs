using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

using Api.Mailgun.Http;

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
        public async Task<Result> CreateMailingListAsync(string alias)
        {
            return await CreateMailingListAsync(alias, alias, null, AccessLevels.Readonly);
        }

        /// <summary>
        /// Creates a new mailing list
        /// </summary>
        /// <param name="alias">Alias name of the mailing list. The result address will looks like 'alias@WorkDomain'.</param>
        /// <param name="name">Display name of the mailing list </param>
        /// <returns></returns>
        public async Task<Result> CreateMailingListAsync(string alias, string name)
        {
            return await CreateMailingListAsync(alias, name, null, AccessLevels.Readonly);
        }

        /// <summary>
        /// Creates a new mailing list
        /// </summary>
        /// <param name="alias">Alias name of the mailing list. The result address will looks like 'alias@WorkDomain'.</param>
        /// <param name="name">Display name of the mailing list </param>
        /// <param name="description">Mailing list description</param>
        /// <returns></returns>
        public async Task<Result> CreateMailingListAsync(string alias, string name, string description)
        {
            return await CreateMailingListAsync(alias, name, description, AccessLevels.Readonly);
        }

        /// <summary>
        /// Creates a new mailing list
        /// </summary>
        /// <param name="alias">Alias name of the mailing list. The result address will looks like 'alias@WorkDomain'.</param>
        /// <param name="name">Display name of the mailing list </param>
        /// <param name="description">Mailing list description</param>
        /// <param name="access">Mailing list access level</param>
        /// <returns></returns>
        public async Task<Result> CreateMailingListAsync(string alias, string name, string description, AccessLevels access)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(alias))
                    throw new ArgumentNullException(nameof(alias));

                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException(nameof(name));
                #endregion

                var uri = $"{BaseUri}/lists";

                var content = new MultipartFormDataContent
                {
                    { "address", $"{alias}@{WorkDomain}" },
                    { "name", name },
                    { "description", description },
                    { "access_level", access.ToString().ToLower() }
                };

                var response = await Http.Client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Update mailing list properties
        /// </summary>
        /// <param name="alias">Alias name of the mailing list</param>
        /// <param name="name">New mailing list display name</param>
        /// <returns></returns>
        public async Task<Result> UpdateMailingListNameAsync(string alias, string name)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(alias))
                    throw new ArgumentNullException(nameof(alias));

                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException(nameof(name));
                #endregion

                var uri = $"{BaseUri}/lists/{alias}@{WorkDomain}";

                var content = new MultipartFormDataContent
                {
                    { "address", $"{alias}@{WorkDomain}" },
                    { "name", name },
                };

                var response = await Http.Client.PutAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Updates mailing list properties
        /// </summary>
        /// <param name="alias">Alias name of the mailing list</param>
        /// <param name="description">New mailing list description</param>
        /// <returns></returns>
        public async Task<Result> UpdateMailingListDescriptionAsync(string alias, string description)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(alias))
                    throw new ArgumentNullException(nameof(alias));

                if (string.IsNullOrEmpty(description))
                    throw new ArgumentNullException(nameof(description));
                #endregion

                var uri = $"{BaseUri}/lists/{alias}@{WorkDomain}";

                var content = new MultipartFormDataContent
                {
                    { "address", $"{alias}@{WorkDomain}" },
                    { "description", description },
                };

                var response = await Http.Client.PutAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Updates mailing list properties
        /// </summary>
        /// <param name="alias">Alias name of the mailing list</param>
        /// <param name="access">New mailing list access level</param>
        /// <returns></returns>
        public async Task<Result> UpdateMailingListAccessAsync(string alias, AccessLevels access)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(alias))
                    throw new ArgumentNullException(nameof(alias));
                #endregion

                var uri = $"{BaseUri}/lists/{alias}@{WorkDomain}";

                var content = new MultipartFormDataContent
                {
                    { "address", $"{alias}@{WorkDomain}" },
                    { "access_level", access.ToString().ToLower() }
                };

                var response = await Http.Client.PutAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a mailing list
        /// </summary>
        /// <param name="alias">Alias name of the mailing list</param>
        /// <returns></returns>
        public async Task<Result> DeleteMailingListAsync(string alias)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(alias))
                    throw new ArgumentNullException(nameof(alias));
                #endregion

                var uri = $"{BaseUri}/lists/{alias}@{WorkDomain}";

                var response = await Http.Client.DeleteAsync(uri);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
        #endregion

        #region Mailing lists members
        /// <summary>
        /// Adds a member to the mailing list
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="email">Email address of the member</param>
        /// <returns></returns>
        public async Task<Result> AddMemberToListAsync(string list, string email)
        {
            return await AddMemberToListAsync(list, email, email);
        }

        /// <summary>
        /// Adds a member to the mailing list
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="email">Email address of the member</param>
        /// <param name="name">Optional member name</param>
        /// <returns></returns>
        public async Task<Result> AddMemberToListAsync(string list, string email, string name)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(list))
                    throw new ArgumentNullException(nameof(list));

                if (string.IsNullOrEmpty(email))
                    throw new ArgumentNullException(nameof(email));

                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException(nameof(name));
                #endregion

                var uri = $"{BaseUri}/lists/{list}@{WorkDomain}/members";

                var content = new MultipartFormDataContent
                {
                    { "address", email },
                    { "name", name },
                    { "subscribed", true },
                    { "upsert", true }
                };

                var response = await Http.Client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Updates a mailing list member with given properties
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="email">Email address of the member</param>
        /// <param name="name">Optional member name</param>
        /// <returns></returns>
        public async Task<Result> UpdateMemberNameAsync(string list, string email, string name)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(list))
                    throw new ArgumentNullException(nameof(list));

                if (string.IsNullOrEmpty(email))
                    throw new ArgumentNullException(nameof(email));

                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException(nameof(name));
                #endregion

                var uri = $"{BaseUri}/lists/{list}@{WorkDomain}/members/{email}";

                var content = new MultipartFormDataContent
                {
                    { "address", email },
                    { "name", name },
                };

                var response = await Http.Client.PutAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Updates a mailing list member with given properties
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="email">Email address of the member</param>
        /// <param name="subscribed">Status of the member subscription</param>
        /// <returns></returns>
        public async Task<Result> UpdateMemberStatusAsync(string list, string email, bool subscribed)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(list))
                    throw new ArgumentNullException(nameof(list));

                if (string.IsNullOrEmpty(email))
                    throw new ArgumentNullException(nameof(email));
                #endregion

                var uri = $"{BaseUri}/lists/{list}@{WorkDomain}/members/{email}";

                var content = new MultipartFormDataContent
                {
                    { "address", email },
                    { "subscribed", subscribed }
                };

                var response = await Http.Client.PutAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a mailing list member
        /// </summary>
        /// <param name="list">Alias name of the mailing list</param>
        /// <param name="email">Email address of the member</param>
        /// <returns></returns>
        public async Task<Result> RemoveMemberFromListAsync(string list, string email)
        {
            try
            {
                #region check args
                if (string.IsNullOrEmpty(list))
                    throw new ArgumentNullException(nameof(list));

                if (string.IsNullOrEmpty(email))
                    throw new ArgumentNullException(nameof(email));
                #endregion

                var uri = $"{BaseUri}/lists/{list}@{WorkDomain}/members/{email}";

                var response = await Http.Client.DeleteAsync(uri);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
        #endregion

        #region Messaging
        /// <summary>
        /// Sends a message by assembling it from the components
        /// </summary>
        /// <param name="message">Mailgun message</param>
        /// <returns></returns>
        public async Task<Result> SendMessageAsync(Message message)
        {
            try
            {
                #region check args
                if (message == null)
                    throw new ArgumentNullException(nameof(message));
                #endregion

                message.EnsureValid();

                var uri = $"{BaseUri}/{WorkDomain}/messages";

                var content = new MultipartFormDataContent
                {
                    { "from", message.From },
                    { "to", message.To },
                    { "cc", message.Cc },
                    { "bcc", message.Bcc },
                    { "subject", message.Subject },
                    { "text", message.Text },
                    { "html", message.Html },
                    { "attachment", message.Attachments },
                    { "inline", message.InlineAttachments },
                    { "o:tag", message.Tags },
                    { "o:dkim", message.Dkim, BoolModes.YesNo },
                    { "o:deliverytime", message.DeliveryTime?.ToString("r") },
                    { "o:testmode", message.TestMode, BoolModes.YesNo },
                    { "o:tracking", message.Tracking, BoolModes.YesNo },
                    { "o:tracking-clicks", message.TrackingClicks, BoolModes.YesNo },
                    { "o:tracking-opens", message.TrackingOpens, BoolModes.YesNo },
                    { "o:require-tls", message.RequireTls },
                    { "o:skip-verification", message.SkipVerification },
                    { message.CustomHeaders },
                    { message.CustomJsonData }
                };

                var response = await Http.Client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    return Result.Fail((int)response.StatusCode);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
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
        public async Task<Result> SendMessageAsync(Member from, Member to, string subject, string html, bool requireTls = false)
        {
            try
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
                    To = new List<Member>(){ to },
                    Subject = subject,
                    Html = html,
                    Dkim = true,
                    RequireTls = requireTls
                };

                return await SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
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
        public async Task<Result> SendMessageToListAsync(string list, Member from, string subject, string html, bool requireTls = false)
        {
            return await SendMessageAsync(from, new Member($"{list}@{WorkDomain}"), subject, html, requireTls);
        }
        #endregion
    }
}
