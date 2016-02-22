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
    public class ApiResponse<T>
    {
        [JsonConverter(typeof(ExtEnumConverter))]
        public ApiResponseStatus Status { get; set; }
        public string Comment { get; set; }
        public T Result { get; set; }
    }
}
