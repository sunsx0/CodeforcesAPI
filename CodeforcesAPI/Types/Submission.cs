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
    /// Represents a submission.
    /// </summary>
    public class Submission
    {
        public int Id { get; set; }
        public int ContestId { get; set; }
        /// <summary>
        /// Time, when submission was created.
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "creationTimeSeconds", Required = Required.Default)]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// Number of seconds, passed after the start of the contest (or a virtual start for virtual parties), before the submission.
        /// </summary>
        public int RelativeTimeSeconds { get; set; }
        public Problem Problem { get; set; }
        public Party Author { get; set; }
        public string ProgrammingLanguage { get; set; }
        /// <summary>
        /// Can be absent.
        /// </summary>
        [JsonConverter(typeof(ExtEnumConverter))]
        public SubmissionVerdict Verdict { get; set; }
        /// <summary>
        /// Testset used for judging the submission.
        /// </summary>
        [JsonConverter(typeof(ExtEnumConverter))]
        public SubmissionTestset TestSet { get; set; }
        /// <summary>
        /// Number of passed tests.
        /// </summary>
        public int PassedTestCount { get; set; }
        /// <summary>
        /// Maximum time in milliseconds, consumed by solution for one test.
        /// </summary>
        public int TimeConsumedMillis { get; set; }
        /// <summary>
        /// Maximum memory in bytes, consumed by solution for one test.
        /// </summary>
        public int MemoryConsumedBytes { get; set; }
    }
}
