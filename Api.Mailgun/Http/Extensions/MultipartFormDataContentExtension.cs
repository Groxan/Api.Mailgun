using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Api.Mailgun.Http
{
    /// <summary>
    /// Extensions for easy addition the mailgun api request fields as multipart form data
    /// </summary>
    public static class MultipartFormDataContentExtension
    {
        /// <summary>
        /// Add bool value as string content. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="key">The name of the content to add</param>
        /// <param name="value">The content to add to the collection</param>
        public static void Add(this MultipartFormDataContent content, string key, bool? value)
        {
            if (value != null)
            {
                content.Add(new StringContent(value.Value ? "True" : "False"), key);
            }
        }

        /// <summary>
        /// Add bool value as string content with specified coversion mode. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="key">The name of the content to add</param>
        /// <param name="value">The content to add to the collection</param>
        /// <param name="mode">Bool to string coversion mode</param>
        public static void Add(this MultipartFormDataContent content, string key, bool? value, BoolModes mode)
        {
            if (value != null)
            {
                if (mode == BoolModes.TrueFalse)
                    content.Add(new StringContent(value.Value ? "True" : "False"), key);
                else
                    content.Add(new StringContent(value.Value ? "yes" : "no"), key);
            }
        }

        /// <summary>
        /// Add string value as string content. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="key">The name of the content to add</param>
        /// <param name="value">The content to add to the collection</param>
        public static void Add(this MultipartFormDataContent content, string key, string value)
        {
            if (value != null)
            {
                content.Add(new StringContent(value), key);
            }
        }

        /// <summary>
        /// Add multiple string values as string contents. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="key">The name of the content to add</param>
        /// <param name="values">The contents to add to the collection</param>
        public static void Add(this MultipartFormDataContent content, string key, IEnumerable<string> values)
        {
            if (values != null)
            {
                foreach (var value in values)
                    content.Add(new StringContent(value), key);
            }
        }

        /// <summary>
        /// Add email member as string content. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="key">The name of the content to add</param>
        /// <param name="value">The content to add to the collection</param>
        public static void Add(this MultipartFormDataContent content, string key, Member value)
        {
            if (value != null)
            {
                content.Add(new StringContent(value.FullName), key);
            }
        }

        /// <summary>
        /// Add multiple email members as string contents. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="key">The name of the content to add</param>
        /// <param name="values">The contents to add to the collection</param>
        public static void Add(this MultipartFormDataContent content, string key, IEnumerable<Member> values)
        {
            if (values != null)
            {
                foreach (var value in values)
                    content.Add(new StringContent(value.FullName), key);
            }
        }

        /// <summary>
        /// Add email atachment as byte array content. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="key">The name of the content to add</param>
        /// <param name="value">The content to add to the collection</param>
        public static void Add(this MultipartFormDataContent content, string key, Attachment value)
        {
            if (value != null)
            {
                content.Add(new ByteArrayContent(value.Data), key, value.FileName);
            }
        }

        /// <summary>
        /// Add multiple email attachments as byte array contents. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="key">The name of the content to add</param>
        /// <param name="values">The contents to add to the collection</param>
        public static void Add(this MultipartFormDataContent content, string key, IEnumerable<Attachment> values)
        {
            if (values != null)
            {
                foreach (var value in values)
                    content.Add(new ByteArrayContent(value.Data), key, value.FileName);
            }
        }

        /// <summary>
        /// Add custom email message header as string content. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="value">The custom email message header to add to the collection. Header name will be prefixed with "h:" automatically.</param>
        public static void Add(this MultipartFormDataContent content, CustomHeader value)
        {
            if (value != null)
            {
                content.Add(new StringContent(value.Value), $"h:{value.Name}");
            }
        }

        /// <summary>
        /// Add multiple custom email message headers as string contents. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="value">The custom email message headers to add to the collection. Headers names will be prefixed with "h:" automatically.</param>
        public static void Add(this MultipartFormDataContent content, IEnumerable<CustomHeader> values)
        {
            if (values != null)
            {
                foreach (var value in values)
                    content.Add(new StringContent(value.Value), $"h:{value.Name}");
            }
        }

        /// <summary>
        /// Add custom email message data as string content. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="value">The custom email message data to add to the collection. Data name will be prefixed with "v:" automatically.</param>
        public static void Add(this MultipartFormDataContent content, CustomData value)
        {
            if (value != null)
            {
                content.Add(new StringContent(value.Data), $"v:{value.Name}");
            }
        }

        /// <summary>
        /// Add multiple custom email message data as string contents. Null value will not be added.
        /// </summary>
        /// <param name="content">Target content</param>
        /// <param name="value">The custom email message data to add to the collection. Data names will be prefixed with "v:" automatically.</param>
        public static void Add(this MultipartFormDataContent content, IEnumerable<CustomData> values)
        {
            if (values != null)
            {
                foreach (var value in values)
                    content.Add(new StringContent(value.Data), $"v:{value.Name}");
            }
        }
    }
}
