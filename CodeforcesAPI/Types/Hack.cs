using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CodeforcesAPI.Helpers;

namespace CodeforcesAPI
{
    /// <summary>
    /// Represents a hack, made during Codeforces Round.
    /// </summary>
    public class Hack
    {
        public int Id { get; set; }
        /// <summary>
        /// Hack creation time.
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "creationTimeSeconds", Required = Required.Default)]
        public DateTime CreationTime { get; set; }
        public Party Hacker { get; set; }
        public Party Defender { get; set; }
        /// <summary>
        /// Can be absent.
        /// </summary>
        [JsonConverter(typeof(ExtEnumConverter))]
        public HackVerdict Verdict { get; set; }
        /// <summary>
        /// Hacked problem.
        /// </summary>
        public Problem Problem { get; set; }
        /// <summary>
        /// Can be absent.
        /// </summary>
        public string Test { get; set; }
        /// <summary>
        /// Localized. Can be absent.
        /// </summary>
        public HackJudgeProtocol JudgeProtocol { get; set; }
    }
}
