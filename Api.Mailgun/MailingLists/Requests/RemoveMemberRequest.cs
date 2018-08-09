using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun.MailingLists
{
    /// <summary>
    /// Response to the mailing list member removing request
    /// </summary>
    [DataContract]
    public class RemoveMemberResponse
    {
        /// <summary>
        /// Email address of the removed member
        /// </summary>
        public string Address
        {
            get => MailingListMember.Address;
            set => MailingListMember.Address = value;
        }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }

        [DataMember(Name = "member")]
        internal RemovedMemberInfo MailingListMember { get; set; }
    }

    [DataContract]
    class RemovedMemberInfo
    {
        [DataMember(Name = "address")]
        public string Address { get; set; }
    }

    class RemoveMemberRequest : HttpRequest<RemoveMemberResponse>
    {
        public RemoveMemberRequest(string baseUri, string workDomain, string list, string member)
        {
            Method = HttpMethod.Delete;
            RequestUri = new Uri($"{baseUri}/lists/{list}@{workDomain}/members/{member}");
        }
    }
}
