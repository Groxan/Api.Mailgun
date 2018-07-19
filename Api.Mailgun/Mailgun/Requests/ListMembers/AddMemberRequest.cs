using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;

using Api.Mailgun.Http;
using Api.Mailgun.Tools;

namespace Api.Mailgun.Requests
{
    /// <summary>
    /// Response to the add mailing list member request
    /// </summary>
    [DataContract]
    public class AddMemberResponse
    {
        /// <summary>
        /// Created mailing list member
        /// </summary>
        [DataMember(Name = "member")]
        public MailingListMember Member { get; set; }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class AddMemberRequest : HttpRequestMessage
    {
        public AddMemberRequest(string baseUri, string workDomain, string list, string email, string name, Dictionary<string, string> vars, bool? subscribed, bool? upsert)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri($"{baseUri}/lists/{list}@{workDomain}/members");
            Content = new MultipartFormDataContent
            {
                { "address", email },
                { "name", name },
                { "vars", Json.Serialize(vars) },
                { "subscribed", subscribed, BoolModes.YesNo },
                { "upsert", upsert, BoolModes.YesNo }
            };
        }
    }
}
