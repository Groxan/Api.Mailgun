using System;
using System.Net.Http;
using System.Runtime.Serialization;

using Api.Mailgun.Http;

namespace Api.Mailgun.Requests
{
    /// <summary>
    /// Response to the mailing list creation request
    /// </summary>
    [DataContract]
    public class CreateMailingListResponse
    {
        /// <summary>
        /// Created mailing list
        /// </summary>
        [DataMember(Name = "list")]
        public MailingList MailingList { get; set; }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class CreateMailingListRequest : HttpRequestMessage
    {
        public CreateMailingListRequest(string baseUri, string workDomain, string alias, string name, string description, AccessLevels access)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri($"{baseUri}/lists");
            Content = new MultipartFormDataContent
            {
                { "address", $"{alias}@{workDomain}" },
                { "name", name },
                { "description", description },
                { "access_level", access.ToString().ToLower() }
            };
        }
    }
}
