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
    /// Represents a recent action.
    /// </summary>
    public class RecentAction
    {
        /// <summary>
        /// Action time
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "timeSeconds", Required = Required.Default)]
        public DateTime Time { get; set; }
        /// <summary>
        /// BlogEntry object in short form. Can be absent.
        /// </summary>
        public BlogEntry BlogEntry { get; set; }
        /// <summary>
        /// Comment object. Can be absent.
        /// </summary>
        public Comment Comment { get; set; }
    }
}
