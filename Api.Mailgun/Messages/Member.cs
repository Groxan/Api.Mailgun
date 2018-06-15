using System;

namespace Api.Mailgun
{
    /// <summary>
    /// Email member with specified address and display name
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Member display name
        /// </summary>
        public string Name
        { get; private set; }

        /// <summary>
        /// Member email address
        /// </summary>
        public string Email
        { get; private set; }

        /// <summary>
        /// Member name, formatted as 'Name <Email>' or 'Email'
        /// </summary>
        public string FullName
            => string.IsNullOrEmpty(Name) ? Email : $"\"{Name}\" <{Email}>";

        /// <summary>
        /// Creates an instance of the Member
        /// </summary>
        /// <param name="email">Member email address</param>
        public Member(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            Email = email;
        }

        /// <summary>
        /// Creates an instance of the Member
        /// </summary>
        /// <param name="email">Member email address</param>
        /// <param name="name">Member display name</param>
        public Member(string email, string name)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            Email = email;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
