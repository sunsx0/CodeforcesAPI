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
    /// Represents a problem.
    /// </summary>
    public class Problem
    {
        /// <summary>
        /// Id of the contest, containing the problem.
        /// </summary>
        public int ContestId { get; set; }
        /// <summary>
        /// Usually a letter of a letter, followed by a digit, that represent a problem index in a contest.
        /// </summary>
        public string Index { get; set; }
        /// <summary>
        /// Localized.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Can be absent. Maximum ammount of points for the problem.
        /// </summary>
        public double Points { get; set; }
        /// <summary>
        /// Problem tags.
        /// </summary>
        public string[] Tags { get; set; }
    }
}
