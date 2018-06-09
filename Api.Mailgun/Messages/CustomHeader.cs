using System;

namespace Api.Mailgun
{
    /// <summary>
    /// Wrapper for the custom email message header
    /// </summary>
    public class CustomHeader
    {
        /// <summary>
        /// Custom header name
        /// </summary>
        public string Name
        { get; private set; }

        /// <summary>
        /// Custom header value
        /// </summary>
        public string Value
        { get; private set; }

        /// <summary>
        /// Creates an instance of the CustomHeader
        /// </summary>
        /// <param name="name">Custom header name</param>
        /// <param name="value">Custom header value</param>
        public CustomHeader(string name, string value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
