namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Route filters helper
    /// </summary>
    public static class RouteFilters
    {
        /// <summary>
        /// Creates a filter that matches SMTP recipient of the message against the specified regex pattern.
        /// </summary>
        /// <param name="pattern">The regex pattern.</param>
        /// <returns>Filter expression string</returns>
        public static string MatchRecipient(string pattern)
        {
            return $"match_recipient(\"{pattern}\")";
        }

        /// <summary>
        /// Creates a filter that matches the MIME header of the message against the specified regex pattern.
        /// </summary>
        /// <param name="header">The MIME header of the message.</param>
        /// <param name="pattern">The regex pattern.</param>
        /// <returns>Filter expression string</returns>
        public static string MatchHeader(string header, string pattern)
        {
            return $"match_header(\"{header}\", \"{pattern}\")";
        }

        /// <summary>
        /// Creates a filter that catch all messages.
        /// </summary>
        /// <returns>Filter expression string</returns>
        public static string CatchAll()
        {
            return "catch_all()";
        }
    }
}
