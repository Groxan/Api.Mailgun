using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun.MailingLists
{
    /// <summary>
    /// Response to the mailing list update request
    /// </summary>
    [DataContract]
    public class UpdateMailingListResponse
    {
        /// <summary>
        /// Updated mailing list
        /// </summary>
        [DataMember(Name = "list")]
        public MailingList MailingList { get; set; }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class UpdateMailingListRequest : HttpRequest<UpdateMailingListResponse>
    {
        public UpdateMailingListRequest(string baseUri, string workDomain, string list, string alias, string name, string description, AccessLevels? access)
        {
            Method = HttpMethod.Put;
            RequestUri = new Uri($"{baseUri}/lists/{list}@{workDomain}");
            Content = new MultipartFormDataContent
            {
                { "address", alias == null ? null : $"{alias}@{workDomain}" },
                { "name", name },
                { "description", description },
                { "access_level", access?.ToString().ToLower() }
            };
        }
    }
}
