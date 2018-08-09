using System.Collections;
using System.Collections.Generic;

namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Route actions helper
    /// </summary>
    public static class RouteActions
    {
        /// <summary>
        /// Creates a Forward action with the specified destination.
        /// <para />This action forwards the message to a specified destination, which can be another email address or a URL.
        /// </summary>
        /// <param name="destination">Destination URL or email address.</param>
        /// <returns>Action string</returns>
        public static string Forward(string destination)
        {
            return $"forward(\"{destination}\")";
        }

        /// <summary>
        /// Creates a Store action with the optional endpoint for notification.
        /// <para />This action stores the message temporarily (for up to 3 days) on Mailgun’s servers so that you can retrieve them later.
        /// </summary>
        /// <param name="callback">URL for receiving notification when the email arrives (optional).</param>
        /// <returns>Action string</returns>
        public static string Store(string callback = null)
        {
            return callback == null ? "store()" : $"store(notify=\"{callback}\")";
        }

        /// <summary>
        /// Creates a Stop action.
        /// <para />This action stops the evaluation of the subsequent Routes. Otherwise, all lower priority Routes will also be evaluated.
        /// </summary>
        /// <returns>Action string</returns>
        public static string Stop()
        {
            return "stop()";
        }

        /// <summary>
        /// Creates a collection of actions and appends a Forward action with the specified destination.
        /// <para />This action forwards the message to a specified destination, which can be another email address or a URL.
        /// </summary>
        /// <param name="destination">Destination URL or email address.</param>
        /// <returns>Collection of actions</returns>
        public static ActionCollection AddForward(string destination)
        {
            return new ActionCollection().AddForward(destination);

        }

        /// <summary>
        /// Creates a collection of actions and appends a Store action with the optional endpoint for notification.
        /// <para />This action stores the message temporarily (for up to 3 days) on Mailgun’s servers so that you can retrieve them later.
        /// </summary>
        /// <param name="callback">URL for receiving notification when the email arrives (optional).</param>
        /// <returns>Collection of actions</returns>
        public static ActionCollection AddStore(string callback = null)
        {
            return new ActionCollection().AddStore(callback);
        }

        /// <summary>
        /// Creates a collection of actions and appends a Stop action.
        /// <para />This action stops the evaluation of the subsequent Routes. Otherwise, all lower priority Routes will also be evaluated.
        /// </summary>
        /// <returns>Collection of actions</returns>
        public static ActionCollection AddStop()
        {
            return new ActionCollection().AddStop();            
        }
    }

    /// <summary>
    /// Helper class for creating an IEnumerable of actions
    /// </summary>
    public class ActionCollection : IEnumerable<string>
    {
        /// <summary>
        /// The resulting list of actions
        /// </summary>
        public List<string> Items { get; private set; } = new List<string>();

        /// <summary>
        /// Appends a Forward action with the specified destination.
        /// <para />This action forwards the message to a specified destination, which can be another email address or a URL.
        /// </summary>
        /// <param name="destination">Destination URL or email address.</param>
        /// <returns>Collection of actions</returns>
        public ActionCollection AddForward(string destination)
        {
            Items.Add(RouteActions.Forward(destination));
            return this;
        }

        /// <summary>
        /// Appends a Store action with the optional endpoint for notification.
        /// <para />This action stores the message temporarily (for up to 3 days) on Mailgun’s servers so that you can retrieve them later.
        /// </summary>
        /// <param name="callback">URL for receiving notification when the email arrives (optional).</param>
        /// <returns>Collection of actions</returns>
        public ActionCollection AddStore(string callback = null)
        {
            Items.Add(RouteActions.Store(callback));
            return this;
        }

        /// <summary>
        /// Appends a Stop action.
        /// <para />This action stops the evaluation of the subsequent Routes. Otherwise, all lower priority Routes will also be evaluated.
        /// </summary>
        /// <returns>Collection of actions</returns>
        public ActionCollection AddStop()
        {
            Items.Add(RouteActions.Stop());
            return this;
        }

        #region IEnumerable
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        #endregion
    }
}
