using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Response to the route creation request
    /// </summary>
    [DataContract]
    public class CreateRouteResponse
    {
        /// <summary>
        /// Created route
        /// </summary>
        [DataMember(Name = "route")]
        public Route Route { get; set; }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class CreateRouteRequest : HttpRequest<CreateRouteResponse>
    {
        public CreateRouteRequest(string baseUri, int? priority, string description, string filter, IEnumerable<string> actions)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri($"{baseUri}/routes");
            Content = new MultipartFormDataContent
            {
                { "priority", priority?.ToString() },
                { "description", description },
                { "expression", filter },
                { "action", actions }
            };            
        }
    }
}
