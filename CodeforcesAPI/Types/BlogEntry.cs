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
    /// Represents a Codeforces blog entry. May be in either short or full version.
    /// </summary>
    public class BlogEntry
    {
        public int Id { get; set; }
        /// <summary>
        /// Original locale of the blog entry.
        /// </summary>
        public string OriginalLocale { get; set; }
        /// <summary>
        /// Time, when blog entry was created.
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "creationTimeSeconds", Required = Required.Default)]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// Author user handle.
        /// </summary>
        public string AuthorHandle { get; set; }
        /// <summary>
        /// Localized.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Localized. Not included in short version.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Time, when blog entry has been updated, in unix format.
        /// </summary>
        public string Locale { get; set; }
        /// <summary>
        /// Time, when blog entry has been updated.
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "modificationTimeSeconds", Required = Required.Default)]
        public DateTime ModificationTimeSeconds { get; set; }
        /// <summary>
        ///  If true, you can view any specific revision of the blog entry.
        /// </summary>
        public bool AllowViewHistory { get; set; }
        public string[] Tags { get; set; }
        public int Rating { get; set; }
    }
}
