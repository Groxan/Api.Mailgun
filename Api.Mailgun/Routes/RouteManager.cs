using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Wrapper for the Routes API
    /// </summary>
    public class RouteManager : IDisposable
    {
        readonly string BaseUri;
        readonly HttpSender Http;

        /// <summary>
        /// Creates an instanse of the RouteManager
        /// </summary>
        /// <param name="apiKey">Your api key</param>
        /// <param name="baseUri">Base uri for api requests</param>
        public RouteManager(string apiKey, string baseUri = "https://api.mailgun.net/v3")
        {
            #region check args
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            if (string.IsNullOrEmpty(baseUri))
                throw new ArgumentNullException(nameof(baseUri));
            #endregion

            BaseUri = baseUri;
            Http = new HttpSender("api", apiKey);
        }

        internal RouteManager(string baseUri, HttpSender http)
        {
            BaseUri = baseUri;
            Http = http;
        }

        #region get
        /// <summary>
        /// Returns a single Route by Id
        /// </summary>
        /// <param name="id">Route Id</param>
        /// <returns></returns>
        public async Task<Result<GetRouteResponse>> GetRouteAsync(string id)
        {
            #region check args
            if (String.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            #endregion

            return await Http.SendRequest(new GetRouteRequest(BaseUri, id));
        }

        /// <summary>
        /// Returns a list of Routes
        /// </summary>
        /// <param name="count">Maximum number of Routes to return</param>
        /// <param name="offset">Number of Routes to skip</param>
        /// <returns></returns>
        public async Task<Result<GetRoutesResponse>> GetRoutesAsync(int count, int offset = 0)
        {
            return await Http.SendRequest(new GetRoutesRequest(BaseUri, offset, count));
        }
        #endregion

        #region create
        /// <summary>
        /// Creates a new Route
        /// </summary>
        /// <param name="filter">Filter expression that determines when Route actions should be triggered</param>
        /// <param name="actions">Actions to be executed when the filter expression evaluates to true</param>
        /// <returns></returns>
        public async Task<Result<CreateRouteResponse>> CreateRouteAsync(string filter, IEnumerable<string> actions)
        {
            #region check args
            if (String.IsNullOrEmpty(filter))
                throw new ArgumentNullException(nameof(filter));
            if (actions == null)
                throw new ArgumentNullException(nameof(actions));
            #endregion

            return await Http.SendRequest(new CreateRouteRequest(BaseUri, null, null, filter, actions));
        }

        /// <summary>
        /// Creates a new Route
        /// </summary>
        /// <param name="filter">Filter expression that determines when Route actions should be triggered</param>
        /// <param name="actions">Actions to be executed when the filter expression evaluates to true</param>
        /// <param name="priority">Route priority (smaller number indicates higher priority)</param>
        /// <returns></returns>
        public async Task<Result<CreateRouteResponse>> CreateRouteAsync(string filter, IEnumerable<string> actions, int priority)
        {
            #region check args
            if (String.IsNullOrEmpty(filter))
                throw new ArgumentNullException(nameof(filter));
            if (actions == null)
                throw new ArgumentNullException(nameof(actions));
            #endregion

            return await Http.SendRequest(new CreateRouteRequest(BaseUri, priority, null, filter, actions));
        }

        /// <summary>
        /// Creates a new Route
        /// </summary>
        /// <param name="filter">Filter expression that determines when Route actions should be triggered</param>
        /// <param name="actions">Actions to be executed when the filter expression evaluates to true</param>
        /// <param name="priority">Route priority (smaller number indicates higher priority)</param>
        /// <param name="description">Route description</param>
        /// <returns></returns>
        public async Task<Result<CreateRouteResponse>> CreateRouteAsync(string filter, IEnumerable<string> actions, int priority, string description)
        {
            #region check args
            if (String.IsNullOrEmpty(filter))
                throw new ArgumentNullException(nameof(filter));
            if (actions == null)
                throw new ArgumentNullException(nameof(actions));
            if (String.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));
            #endregion

            return await Http.SendRequest(new CreateRouteRequest(BaseUri, priority, description, filter, actions));
        }
        #endregion

        #region update
        /// <summary>
        /// Updates the Route
        /// </summary>
        /// <param name="route">Route</param>
        /// <returns></returns>
        public async Task<Result<UpdateRouteResponse>> UpdateRouteAsync(Route route)
        {
            #region check args
            if (route == null)
                throw new ArgumentNullException(nameof(route));
            #endregion

            return await Http.SendRequest(new UpdateRouteRequest(BaseUri, route.Id, route.Priority, route.Description, route.Filter, route.Actions));
        }

        /// <summary>
        /// Updates the Route priority
        /// </summary>
        /// <param name="route">Route Id</param>
        /// <param name="priority">New priority</param>
        /// <returns></returns>
        public async Task<Result<UpdateRouteResponse>> UpdateRoutePriorityAsync(string route, int priority)
        {
            #region check args
            if (String.IsNullOrEmpty(route))
                throw new ArgumentNullException(nameof(route));
            #endregion

            return await Http.SendRequest(new UpdateRouteRequest(BaseUri, route, priority, null, null, null));
        }

        /// <summary>
        /// Updates the Route description
        /// </summary>
        /// <param name="route">Route Id</param>
        /// <param name="description">New description</param>
        /// <returns></returns>
        public async Task<Result<UpdateRouteResponse>> UpdateRouteDescriptionAsync(string route, string description)
        {
            #region check args
            if (String.IsNullOrEmpty(route))
                throw new ArgumentNullException(nameof(route));
            if (String.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));
            #endregion

            return await Http.SendRequest(new UpdateRouteRequest(BaseUri, route, null, description, null, null));
        }

        /// <summary>
        /// Updates the Route filter
        /// </summary>
        /// <param name="route">Route Id</param>
        /// <param name="filter">New filter</param>
        /// <returns></returns>
        public async Task<Result<UpdateRouteResponse>> UpdateRouteFilterAsync(string route, string filter)
        {
            #region check args
            if (String.IsNullOrEmpty(route))
                throw new ArgumentNullException(nameof(route));
            if (String.IsNullOrEmpty(filter))
                throw new ArgumentNullException(nameof(filter));
            #endregion

            return await Http.SendRequest(new UpdateRouteRequest(BaseUri, route, null, null, filter, null));
        }

        /// <summary>
        /// Updates Route actions
        /// </summary>
        /// <param name="route">Route Id</param>
        /// <param name="actions">New actions</param>
        /// <returns></returns>
        public async Task<Result<UpdateRouteResponse>> UpdateRouteActionsAsync(string route, IEnumerable<string> actions)
        {
            #region check args
            if (String.IsNullOrEmpty(route))
                throw new ArgumentNullException(nameof(route));
            if (actions == null)
                throw new ArgumentNullException(nameof(actions));
            #endregion

            return await Http.SendRequest(new UpdateRouteRequest(BaseUri, route, null, null, null, actions));
        }
        #endregion

        #region delete
        /// <summary>
        /// Deletes the Route
        /// </summary>
        /// <param name="route">Route Id</param>
        /// <returns></returns>
        public async Task<Result<DeleteRouteResponse>> DeleteRouteAsync(string route)
        {
            #region check args
            if (String.IsNullOrEmpty(route))
                throw new ArgumentNullException(nameof(route));
            #endregion

            return await Http.SendRequest(new DeleteRouteRequest(BaseUri, route));
        }
        #endregion

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources
        /// </summary>
        public void Dispose()
        {
            Http.Dispose();
        }
    }
}
