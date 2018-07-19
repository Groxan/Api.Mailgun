using System;

namespace Api.Mailgun
{
    /// <summary>
    /// Email address with specified address and display name
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Email address
        /// </summary>
        public string Address
        { get; private set; }

        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName
        { get; private set; }

        /// <summary>
        /// Creates an instance of the EmailAddress
        /// </summary>
        /// <param name="address">Email address</param>
        public EmailAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            Address = address;
        }

        /// <summary>
        /// Creates an instance of the EmailAddress
        /// </summary>
        /// <param name="address">Email address</param>
        /// <param name="name">Display name</param>
        public EmailAddress(string address, string name)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            DisplayName = name;
            Address = address;
        }

        /// <summary>
        /// Return a string formatted as '"Name" <Address>' or 'Address'
        /// </summary>
        /// <returns>Email address string</returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(DisplayName) ? Address : $"\"{DisplayName}\" <{Address}>";
        }
    }
}
