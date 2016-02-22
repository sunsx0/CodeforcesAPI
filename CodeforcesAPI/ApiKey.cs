using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeforcesAPI.Helpers;
using System.Security.Cryptography;
using System.Net;

namespace CodeforcesAPI
{
    public class ApiKey
    {
        public string Key { get; private set; }
        public string Secret { get; private set; }
        public ApiKey(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }

        protected virtual string GetRand()
        {
            return (DateTime.Now.Ticks % 1000000L).ToString("000000");
        }
        protected virtual long GetTime()
        {
            return DateTime.Now.ToUnixTime() - (long)TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds;
        }
        public virtual void AddSignature(string methodName, Dictionary<string, object> parameters)
        {
            var apiKey = Key;
            var time = GetTime().ToString();
            var rand = GetRand();

            parameters["apiKey"] = apiKey;
            parameters["time"] = time;

            var req = UrlHelper.ToGetRequest(parameters.OrderBy(x => x.Key));

            var str = rand + "/" + methodName + "?" + req + "#" + Secret;

            var hex = string.Empty;
            using (var sha512 = SHA512.Create())
            {
                var hash = sha512.ComputeHash(Encoding.ASCII.GetBytes(str));
                hex = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }

            parameters["apiSig"] = rand + hex;
        }
    }
}
