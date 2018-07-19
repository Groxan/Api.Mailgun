using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace Api.Mailgun.Tests
{
    public static class Settings
    {
        static string _ApiKey = null;
        public static string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(_ApiKey))
                    Read();

                return _ApiKey;
            }
        }

        static string _WorkDomain = null;
        public static string WorkDomain
        {
            get
            {
                if (string.IsNullOrEmpty(_WorkDomain))
                    Read();

                return _WorkDomain;
            }
        }

        static string _Sender = null;
        public static string Sender
        {
            get
            {
                if (string.IsNullOrEmpty(_Sender))
                    Read();

                return _Sender;
            }
        }

        static string _Recipient = null;
        public static string Recipient
        {
            get
            {
                if (string.IsNullOrEmpty(_Recipient))
                    Read();

                return _Recipient;
            }
        }

        static object Crit = new object();

        static void Read()
        {
            lock (Crit)
            {
                using (var fstream = new FileStream("Settings\\tests.settings.json", FileMode.Open))
                {
                    var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true });
                    var settings = (Dictionary<string, string>)serializer.ReadObject(fstream);

                    if (!settings.TryGetValue("ApiKey", out _ApiKey) ||
                        !settings.TryGetValue("WorkDomain", out _WorkDomain) ||
                        !settings.TryGetValue("Sender", out _Sender) ||
                        !settings.TryGetValue("Recipient", out _Recipient))
                        throw new Exception("Can't read settings file");
                }
            }
        }
    }
}
