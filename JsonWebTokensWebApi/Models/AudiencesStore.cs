using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Security.Cryptography;
using JsonWebTokensWebApi.Entities;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace JsonWebTokensWebApi.Models
{
    public static class AudiencesStore
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudiencesStore()
        {
            var defaultClientId = ConfigurationManager.AppSettings["DefaultClientId"];
            var defaultClientSecret = ConfigurationManager.AppSettings["DefaultClientSecret"];
            var defaultClientName = ConfigurationManager.AppSettings["DefaultClientName"];

            AudiencesList.TryAdd(defaultClientId,
                                new Audience
                                {
                                    ClientId = defaultClientId,
                                    Base64Secret = defaultClientSecret,
                                    Name = defaultClientName
                                });
        }

        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RandomNumberGenerator.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            var newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static Audience FindAudience(string clientId)
        {
            Audience audience;
            return AudiencesList.TryGetValue(clientId, out audience) ? audience : null;
        }
    }
}