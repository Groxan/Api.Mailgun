using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Mailgun.MailingLists
{
    /// <summary>
    /// Wrapper for the Mailing list API
    /// </summary>
    public class MailingListManager : IDisposable
    {
        readonly string BaseUri;
        readonly string WorkDomain;
        readonly HttpSender Http;

        /// <summary>
        /// Creates an instanse of the Mailing list manager
        /// </summary>
        /// <param name="workDomain">Your sending domain</param>
        /// <param name="apiKey">Your api key</param>
        /// <param name="baseUri">Base uri for api requests</param>
        public MailingListManager(string workDomain, string apiKey, string baseUri = "https://api.mailgun.net/v3")
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

        internal MailingListManager(string workDomain, string baseUri, HttpSender http)
        {
            BaseUri = baseUri;
            WorkDomain = workDomain;
            Http = http;
        }

        #region create list
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

            return await Http.SendRequest(new CreateMailingListRequest(BaseUri, WorkDomain, alias, null, null, AccessLevels.Readonly));
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

            return await Http.SendRequest(new CreateMailingListRequest(BaseUri, WorkDomain, alias, name, null, AccessLevels.Readonly));
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

            return await Http.SendRequest(new CreateMailingListRequest(BaseUri, WorkDomain, alias, name, description, AccessLevels.Readonly));
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

            return await Http.SendRequest(new CreateMailingListRequest(BaseUri, WorkDomain, alias, name, description, access));
        }
        #endregion

        #region update list
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

            return await Http.SendRequest(new UpdateMailingListRequest(BaseUri, WorkDomain, list, alias, null, null, null));
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

            return await Http.SendRequest(new UpdateMailingListRequest(BaseUri, WorkDomain, list, null, name, null, null));
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

            return await Http.SendRequest(new UpdateMailingListRequest(BaseUri, WorkDomain, list, null, null, description, null));
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

            return await Http.SendRequest(new UpdateMailingListRequest(BaseUri, WorkDomain, list, null, null, null, access));
        }
        #endregion

        #region delete list
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

            return await Http.SendRequest(new DeleteMailingListRequest(BaseUri, WorkDomain, list));
        }
        #endregion

        #region add member
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

            return await Http.SendRequest(new AddMemberRequest(BaseUri, WorkDomain, list, email, null, null, true, true));
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

            return await Http.SendRequest(new AddMemberRequest(BaseUri, WorkDomain, list, email, name, null, true, true));
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

            return await Http.SendRequest(new AddMemberRequest(BaseUri, WorkDomain, list, email, name, vars, true, true));
        }
        #endregion

        #region update member
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

            return await Http.SendRequest(new UpdateMemberRequest(BaseUri, WorkDomain, list, member, email, null, null, null));
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

            return await Http.SendRequest(new UpdateMemberRequest(BaseUri, WorkDomain, list, member, null, name, null, null));
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

            return await Http.SendRequest(new UpdateMemberRequest(BaseUri, WorkDomain, list, member, null, null, vars, null));
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

            return await Http.SendRequest(new UpdateMemberRequest(BaseUri, WorkDomain, list, member, null, null, null, subscribed));
        }
        #endregion

        #region remove member
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

            return await Http.SendRequest(new RemoveMemberRequest(BaseUri, WorkDomain, list, member));
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
