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
    public class Comment
    {
        /// <summary>
        /// Represents a comment.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Time, when comment was created
        /// </summary>
        [JsonConverter(typeof(ExtUnixTimeConverter))]
        [JsonProperty(PropertyName = "creationTimeSeconds", Required = Required.Default)]
        public DateTime CreationTime { get; set; }
        public string CommentatorHandle { get; set; }
        public string Locale { get; set; }
        public string Text { get; set; }
        /// <summary>
        /// Can be absent.
        /// </summary>
        public int ParentCommentId { get; set; }
        public int Rating { get; set; }
    }
}
