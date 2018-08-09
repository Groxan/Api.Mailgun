using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Response to the get routes request
    /// </summary>
    [DataContract]
    public class GetRoutesResponse
    {
        /// <summary>
        /// Total count of the routes
        /// </summary>
        [DataMember(Name = "total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Returned routes
        /// </summary>
        [DataMember(Name = "items")]
        public List<Route> Routes { get; set; }
    }

    class GetRoutesRequest : HttpRequest<GetRoutesResponse>
    {
        public GetRoutesRequest(string baseUri, int skip, int limit)
        {
            Method = HttpMethod.Get;
            RequestUri = new Uri($"{baseUri}/routes?skip={skip}&limit={limit}");
        }
    }
}
