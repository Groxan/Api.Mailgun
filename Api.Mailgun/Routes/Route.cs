using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Api.Mailgun.Routes
{
    /// <summary>
    /// Route to handle incoming emails
    /// </summary>
    [DataContract]
    public class Route
    {
        /// <summary>
        /// Route Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Route description
        /// Note that the Mailgun API has a bug (I think so): when the description is empty, Mailgun will return this field with a value of "false", not "".
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Route creation time
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Actions to be executed when the filter expression evaluates to true
        /// </summary>
        [DataMember(Name = "actions")]
        public List<string> Actions { get; set; }

        /// <summary>
        /// Filter expression that determines when Route actions should be triggered
        /// </summary>
        [DataMember(Name = "expression")]
        public string Filter { get; set; }

        /// <summary>
        /// Route priority (smaller number indicates higher priority)
        /// </summary>
        [DataMember(Name = "priority")]
        public int Priority { get; set; }
        
        #region internal
        [DataMember(Name = "created_at")]
        internal string CreationTimeString
        {
            get => CreationTime.ToString();
            set => CreationTime = DateTime.Parse(value);
        }
        #endregion
    }
}
