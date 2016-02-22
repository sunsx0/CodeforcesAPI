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
    /// Represents a ranklist row.
    /// </summary>
    public class RanklistRow
    {
        /// <summary>
        /// Party that took a corresponding place in the contest.
        /// </summary>
        public Party Party { get; set; }
        /// <summary>
        /// Party place in the contest.
        /// </summary>
        public int Rank { get; set; }
        /// <summary>
        /// Total ammount of points, scored by the party.
        /// </summary>
        public float Points { get; set; }
        /// <summary>
        /// Total penalty (in ICPC meaning) of the party.
        /// </summary>
        public int Penalty { get; set; }
        public int SuccessfulHackCount { get; set; }
        public int UnsuccessfulHackCount { get; set; }
        /// <summary>
        /// Party results for each problem. Order of the problems is the same as in "problems" field of the returned object.
        /// </summary>
        public ProblemResult[] ProblemResults { get; set; }
        /// <summary>
        /// For IOI contests only. Time in seconds from the start of the contest to the last submission that added some points to the total score of the party.
        /// </summary>
        public int LastSubmissionTimeSeconds { get; set; }

    }
}
