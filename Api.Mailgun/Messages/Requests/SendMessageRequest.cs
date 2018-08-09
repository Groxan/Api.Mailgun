using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Api.Mailgun
{
    /// <summary>
    /// Response to the send message request
    /// </summary>
    [DataContract]
    public class SendMessageResponse
    {
        /// <summary>
        /// Id of the sent message
        /// </summary>
        [DataMember(Name = "id")]
        public string MessageId { get; set; }

        /// <summary>
        /// Response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Status { get; set; }
    }

    class SendMessageRequest : HttpRequest<SendMessageResponse>
    {
        public SendMessageRequest(string baseUri, string workDomain, Message message)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri($"{baseUri}/{workDomain}/messages");
            Content = new MultipartFormDataContent
            {
                { "from", message.From },
                { "to", message.To },
                { "cc", message.Cc },
                { "bcc", message.Bcc },
                { "subject", message.Subject },
                { "text", message.Text },
                { "html", message.Html },
                { "attachment", message.Attachments },
                { "inline", message.InlineAttachments },
                { "o:tag", message.Tags },
                { "o:dkim", message.Dkim, BoolModes.YesNo },
                { "o:deliverytime", message.DeliveryTime?.ToString("r") },
                { "o:testmode", message.TestMode, BoolModes.YesNo },
                { "o:tracking", message.Tracking, BoolModes.YesNo },
                { "o:tracking-clicks", message.TrackingClicks, BoolModes.YesNo },
                { "o:tracking-opens", message.TrackingOpens, BoolModes.YesNo },
                { "o:require-tls", message.RequireTls },
                { "o:skip-verification", message.SkipVerification },
                { message.CustomHeaders },
                { message.CustomJsonData }
            };
        }
    }
}
