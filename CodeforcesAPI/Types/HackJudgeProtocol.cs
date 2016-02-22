using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CodeforcesAPI.Helpers;

namespace CodeforcesAPI
{
    public class HackJudgeProtocol
    {
        /// <summary>
        /// If manual is "true" then test for the hack was entered manually. 
        /// </summary>
        public bool Manual { get; set; }
        /// <summary>
        /// Contain human-readable description of judge protocol. Localized. Can be absent.
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// Contain human-readable description of hack verdict. Localized. Can be absent.
        /// </summary>
        public string Verdict { get; set; }
    }
}
