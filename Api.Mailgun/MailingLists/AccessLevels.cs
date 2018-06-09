using System;

namespace Api.Mailgun
{
    /// <summary>
    /// Defines how users can interact with the mailing list
    /// </summary>
    public enum AccessLevels
    {
        /// <summary>
        /// Only authenticated users can post to this list. It is used for mass announcements and newsletters.
        /// </summary>
        Readonly,

        /// <summary>
        /// Subscribed members of the list can communicate with each other.
        /// </summary>
        Members,

        /// <summary>
        /// Everyone can post to this list.
        /// </summary>
        Everyone
    }
}
