using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Response to the route update request
    /// </summary>
    [DataContract]
    public class UpdateRouteResponse : Route
    {
        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class UpdateRouteRequest : HttpRequest<UpdateRouteResponse>
    {
        public UpdateRouteRequest(string baseUri, string id, int? priority, string description, string filter, IEnumerable<string> actions)
        {
            Method = HttpMethod.Put;
            RequestUri = new Uri($"{baseUri}/routes/{id}");
            Content = new MultipartFormDataContent
            {
                { "id", id },
                { "priority", priority?.ToString() },
                { "description", description },
                { "expression", filter },
                { "action", actions }
            };
        }
    }
}
