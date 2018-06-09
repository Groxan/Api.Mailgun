using System;

namespace Api.Mailgun
{
    /// <summary>
    /// Result of the Mailgun API request
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Is successful
        /// </summary>
        public bool Successful { get; private set; }

        /// <summary>
        /// Error message if the request was failed
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Creates the successful result
        /// </summary>
        /// <returns>Successful result</returns>
        public static Result Success() => new Result() { Successful = true, ErrorMessage = "" };

        /// <summary>
        /// Creates the failed result
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns>Failed result</returns>
        public static Result Fail(string message) => new Result() { Successful = false, ErrorMessage = message };

        /// <summary>
        /// Creates the failed result
        /// </summary>
        /// <param name="statusCode">Response status code</param>
        /// <returns>Failed result</returns>
        public static Result Fail(int statusCode)
        {
            var message = "";
            switch(statusCode)
            {
                case 400:
                    message = "Bad Request - Often missing a required parameter";
                    break;
                case 401:
                    message = "Unauthorized - No valid API key provided";
                    break;
                case 402:
                    message = "Request Failed - Parameters were valid but request failed";
                    break;
                case 404:
                    message = "Not Found - The requested item doesn’t exist";
                    break;
                case 500:
                case 502:
                case 503:
                case 504:
                    message = "Server Errors - something is wrong on Mailgun’s end";
                    break;
                default:
                    message = $"Unknown response status code: {statusCode}";
                    break;
            }

            return Fail(message);
        }

        public static explicit operator bool(Result result) => result.Successful;
    }
}
