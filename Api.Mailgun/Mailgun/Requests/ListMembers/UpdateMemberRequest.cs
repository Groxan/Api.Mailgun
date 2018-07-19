using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;

using Api.Mailgun.Http;
using Api.Mailgun.Tools;

namespace Api.Mailgun.Requests
{
    /// <summary>
    /// Response to the mailing list member update request
    /// </summary>
    [DataContract]
    public class UpdateMemberResponse
    {
        /// <summary>
        /// Updated mailing list member
        /// </summary>
        [DataMember(Name = "member")]
        public MailingListMember Member { get; set; }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class UpdateMemberRequest : HttpRequestMessage
    {
        public UpdateMemberRequest(string baseUri, string workDomain, string list, string member, string email, string name, Dictionary<string, string> vars, bool? subscribed)
        {
            Method = HttpMethod.Put;
            RequestUri = new Uri($"{baseUri}/lists/{list}@{workDomain}/members/{member}");
            Content = new MultipartFormDataContent
            {
                { "address", email },
                { "name", name },
                { "vars", Json.Serialize(vars) },
                { "subscribed", subscribed, BoolModes.YesNo }
            };
        }
    }
}
