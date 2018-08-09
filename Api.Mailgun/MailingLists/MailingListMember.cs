using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Api.Mailgun.MailingLists
{
    /// <summary>
    /// Mailing list member
    /// </summary>
    [DataContract]
    public class MailingListMember
    {
        /// <summary>
        /// Email address of the member
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Member name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Dictionary with additional parameters, e.g. {"gender", "female"}, {"age", "27"}
        /// </summary>
        [DataMember(Name = "vars")]
        public Dictionary<string, string> Vars { get; set; }

        /// <summary>
        /// Subscription status
        /// </summary>
        [DataMember(Name = "subscribed")]
        public bool Subscribed { get; set; }
    }
}
