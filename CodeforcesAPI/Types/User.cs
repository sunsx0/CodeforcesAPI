using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CodeforcesAPI.Helpers;

namespace CodeforcesAPI
{
    /// <summary>
    /// Represents a Codeforces user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Codeforces user handle.
        /// </summary>
        public string Handle { get; set; }
        /// <summary>
        /// Shown only if user allowed to share his contact info.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User id for VK social network. Shown only if user allowed to share his contact info.
        /// </summary>
        public string VkId { get; set; }
        /// <summary>
        /// Shown only if user allowed to share his contact info.
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// Localized. Can be absent.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Localized. Can be absent.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Localized. Can be absent.
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Localized. Can be absent.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Localized. Can be absent.
        /// </summary>
        public string Organization { get; set; }
        /// <summary>
        /// User contribution.
        /// </summary>
        public int Contribution { get; set; }
        /// <summary>
        /// User rank. Localized.
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// User rating.
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// User max rank. Localized.
        /// </summary>
        public string MaxRank { get; set; }
        /// <summary>
        /// User max rating
        /// </summary>
        public int MaxRating { get; set; }
        /// <summary>
        /// Time, when user was last seen online.
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "lastOnlineTimeSeconds", Required = Required.Default)]
        public DateTime LastOnlineTime { get; set; }
        /// <summary>
        /// Time, when user was registered.
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "registrationTimeSeconds", Required = Required.Default)]
        public DateTime RegistrationTime { get; set; }
        /// <summary>
        /// Amount of users who have this user in friends.
        /// </summary>
        public int FriendOfCount { get; set; }
        /// <summary>
        /// User's avatar URL.
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// User's title photo URL.
        /// </summary>
        public string TitlePhoto { get; set; }
    }
}
