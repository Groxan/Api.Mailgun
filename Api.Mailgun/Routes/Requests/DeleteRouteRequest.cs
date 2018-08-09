using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Response to the route deletion request
    /// </summary>
    [DataContract]
    public class DeleteRouteResponse
    {
        /// <summary>
        /// Id of the deleted route
        /// </summary>
        [DataMember(Name = "id")]
        public string RouteId { get; set; }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class DeleteRouteRequest : HttpRequest<DeleteRouteResponse>
    {
        public DeleteRouteRequest(string baseUri, string route)
        {
            Method = HttpMethod.Delete;
            RequestUri = new Uri($"{baseUri}/routes/{route}");
        }
    }
}
