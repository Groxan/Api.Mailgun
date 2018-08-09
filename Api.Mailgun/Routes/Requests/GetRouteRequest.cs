using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Response to the get route request
    /// </summary>
    [DataContract]
    public class GetRouteResponse
    {
        /// <summary>
        /// Returned route
        /// </summary>
        [DataMember(Name = "route")]
        public Route Route { get; set; }
    }

    class GetRouteRequest : HttpRequest<GetRouteResponse>
    {
        public GetRouteRequest(string baseUri, string id)
        {
            Method = HttpMethod.Get;
            RequestUri = new Uri($"{baseUri}/routes/{id}");
        }
    }
}
