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
    /// Represents a party, participating in a contest.
    /// </summary>
    public class Party
    {
        /// <summary>
        /// Id of the contest, in which party is participating.
        /// </summary>
        public int ContestId { get; set; }
        /// <summary>
        /// Members of the party.
        /// </summary>
        public Member[] Members { get; set; }
        /// <summary>
        /// Can be absent. If party is a team, then it is a unique team id. Otherwise, this field is absent.
        /// </summary>
        [JsonConverter(typeof(ExtEnumConverter))]
        public PartyParticipantType ParticipantType { get; set; }
        /// <summary>
        /// Can be absent. If party is a team, then it is a unique team id. Otherwise, this field is absent.
        /// </summary>
        public int TeamId { get; set; }
        /// <summary>
        /// Localized. Can be absent. If party is a team or ghost, then it is a localized name of the team. Otherwise, it is absent.
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// If true then this party is a ghost. It participated in the contest, but not on Codeforces. For example, Andrew Stankevich Contests in Gym has ghosts of the participants from Petrozavodsk Training Camp.
        /// </summary>
        public bool Ghost { get; set; }
        /// <summary>
        /// Can be absent. Room of the party. If absent, then the party has no room.
        /// </summary>
        public int Room { get; set; }
        /// <summary>
        /// Can be absent. Time, when this party started a contest.
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "startTimeSeconds", Required = Required.Default)]
        public DateTime StartTime { get; set; }
    }
}
