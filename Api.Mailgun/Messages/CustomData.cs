using System;

namespace Api.Mailgun
{
    /// <summary>
    /// Wrapper for the custom email message data
    /// </summary>
    public class CustomData
    {
        /// <summary>
        /// Custom data name
        /// </summary>
        public string Name
        { get; private set; }

        /// <summary>
        /// Custom data in json format
        /// </summary>
        public string Data
        { get; private set; }

        /// <summary>
        /// Creates an instance of the CustomJsonData
        /// </summary>
        /// <param name="name">Custom data name</param>
        /// <param name="data">Custom data in json format. If exceeds 998 characters, you should use folding to spread the variables over multiple lines.</param>
        public CustomData(string name, string data)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}
