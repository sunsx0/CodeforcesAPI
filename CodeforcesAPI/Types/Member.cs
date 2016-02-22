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
    /// Represents a member of a party.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Codeforces user handle
        /// </summary>
        public string Handle { get; set; }
    }
}
