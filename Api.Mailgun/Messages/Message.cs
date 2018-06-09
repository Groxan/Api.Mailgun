using System;
using System.Collections.Generic;

namespace Api.Mailgun
{
    /// <summary>
    /// Email message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Sender of the message
        /// </summary>
        public Member From
        { get; set; }

        /// <summary>
        /// Recipient(s) of the message
        /// </summary>
        public List<Member> To
        { get; set; }

        /// <summary>
        /// Cc (carbon copy) recipient(s) of the message
        /// </summary>
        public List<Member> Cc
        { get; set; }

        /// <summary>
        /// Bcc (blind carbon copy) recipient(s) of the message
        /// </summary>
        public List<Member> Bcc
        { get; set; }

        /// <summary>
        /// Subject of the message
        /// </summary>
        public string Subject
        { get; set; }

        /// <summary>
        /// Body of the message - text version. If Html is provided, this field will be ignored.
        /// </summary>
        public string Text
        { get; set; }

        /// <summary>
        /// Body of the message - HTML version
        /// </summary>
        public string Html
        { get; set; }

        /// <summary>
        /// Email attachments. The maximum message size is 25 MB.
        /// </summary>
        public List<Attachment> Attachments
        { get; set; }

        /// <summary>
        /// Attachments with inline disposition - can be used to send inline images.
        /// Example: <img src="cid:cartman.jpg"> - image is referenced in HTML part simply by the filename.
        /// The maximum message size is 25 MB.
        /// </summary>
        public List<Attachment> InlineAttachments
        { get; set; }

        /// <summary>
        /// Tags of the message. Maximum 3 tags per message, and 4,000 tags per account.
        /// </summary>
        public List<string> Tags
        { get; set; }

        /// <summary>
        /// Enables/disables DKIM signatures on per-message basis
        /// </summary>
        public bool? Dkim
        { get; set; }

        /// <summary>
        /// Desired time of delivery. Messages can be scheduled for a maximum of 3 days in the future.
        /// </summary>
        public DateTime? DeliveryTime
        { get; set; }

        /// <summary>
        /// Enables sending in test mode. Mailgun will accept the message but will not send it.
        /// </summary>
        public bool? TestMode
        { get; set; }

        /// <summary>
        /// Toggles tracking on a per-message basis
        /// </summary>
        public bool? Tracking
        { get; set; }

        /// <summary>
        /// Toggles clicks tracking on a per-message basis. Has higher priority than domain-level setting.
        /// </summary>
        public bool? TrackingClicks
        { get; set; }

        /// <summary>
        /// Toggles opens tracking on a per-message basis. Has higher priority than domain-level setting.
        /// </summary>
        public bool? TrackingOpens
        { get; set; }

        /// <summary>
        /// If set to True this requires the message only be sent over a TLS connection. If a TLS connection can not be established, Mailgun will not deliver the message.
        /// If set to False, Mailgun will still try and upgrade the connection, but if Mailgun can not, the message will be delivered over a plaintext SMTP connection.
        /// The default is False.
        /// </summary>
        public bool? RequireTls
        { get; set; }

        /// <summary>
        /// If set to True, the certificate and hostname will not be verified when trying to establish a TLS connection and Mailgun will accept any certificate during delivery.
        /// If set to False, Mailgun will verify the certificate and hostname.If either one can not be verified, a TLS connection will not be established.
        /// The default is False.
        /// </summary>
        public bool? SkipVerification
        { get; set; }

        /// <summary>
        /// Values to append as custom MIME headers to the message. Value will be prefixed with "h:". For example, "h:Reply-To" to specify Reply-To address.
        /// </summary>
        public List<CustomHeader> CustomHeaders
        { get; set; }

        /// <summary>
        /// Values to attach a custom JSON data to the message. Value will be prefixed with "v:". For example, "v:my-custom-data" => {"first_name": "Agent", "last_name": "Smith"}.
        /// If value exceeds 998 characters, you should use folding to spread the variables over multiple lines.
        /// </summary>
        public List<CustomData> CustomJsonData
        { get; set; }
        
        /// <summary>
        /// Ensure that message has valid fields. Otherwise, ArgumentException will be thrown.
        /// </summary>
        public void EnsureValid()
        {
            if (From == null)
                throw new ArgumentException("No sender", nameof(From));

            if (To == null || To.Count == 0)
                throw new ArgumentException("No recipient", nameof(To));

            if (string.IsNullOrEmpty(Subject))
                throw new ArgumentException("No message subject", nameof(Subject));

            if (string.IsNullOrEmpty(Text) && string.IsNullOrEmpty(Html))
                throw new ArgumentException("No message body", nameof(Text));

            if (Tags != null && Tags.Count > 3)
                throw new ArgumentException("Too many tags", nameof(Tags));

            if (DeliveryTime != null)
            {
                if (DeliveryTime < DateTime.Now)
                    throw new ArgumentException("Invalid delivery time", nameof(DeliveryTime));

                if ((DeliveryTime - DateTime.Now).Value.TotalDays > 3)
                    throw new ArgumentException("Maximum delivery time is exceeded", nameof(DeliveryTime));
            }

            long size = 0;

            if (Attachments != null)
            {
                foreach (var attachment in Attachments)
                    size += attachment.Data.Length;
            }

            if (InlineAttachments != null)
            {
                foreach (var attachment in InlineAttachments)
                    size += attachment.Data.Length;
            }
            
            if (size > 25 * 1024 * 1024)
                throw new ArgumentException("Maximum message size is exceeded");
        }
    }
}
