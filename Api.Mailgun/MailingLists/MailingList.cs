using System;
using System.Runtime.Serialization;

namespace Api.Mailgun
{
    /// <summary>
    /// Mailing list
    /// </summary>
    [DataContract]
    public class MailingList
    {
        /// <summary>
        /// Email address of the mailing list
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Mailing list name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Mailing list description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Mailing list creation datetime
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Mailing list access level
        /// </summary>
        public AccessLevels AccessLevel { get; set; }

        /// <summary>
        /// Current number of members
        /// </summary>
        [DataMember(Name = "members_count")]
        public int MembersCount { get; set; }


        #region internal
        [DataMember(Name = "created_at")]
        internal string CreationTimeString
        {
            get => CreationTime.ToString();
            set => CreationTime = DateTime.Parse(value);
        }

        [DataMember(Name = "access_level")]
        internal string AccessLevelString
        {
            get => AccessLevel.ToString().ToLower();
            set => AccessLevel = value == "members" ? AccessLevels.Members : value == "everyone" ? AccessLevels.Everyone : AccessLevels.Readonly;
        }
        #endregion
    }
}
