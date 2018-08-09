using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun.MailingLists
{
    /// <summary>
    /// Response to the mailing list deletion request
    /// </summary>
    [DataContract]
    public class DeleteMailingListResponse
    {
        /// <summary>
        /// Address of the deleted mailing list
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class DeleteMailingListRequest : HttpRequest<DeleteMailingListResponse>
    {
        public DeleteMailingListRequest(string baseUri, string workDomain, string list)
        {
            Method = HttpMethod.Delete;
            RequestUri = new Uri($"{baseUri}/lists/{list}@{workDomain}");
        }
    }
}
