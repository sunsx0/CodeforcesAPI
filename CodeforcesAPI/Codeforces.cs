using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Globalization;
using CodeforcesAPI.Methods;
using CodeforcesAPI.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace CodeforcesAPI
{
    public class Codeforces
    {
        public const string ApiUrl = "http://codeforces.com/api/";

        public BlogEntryMethods BlogEntry { get; private set; }
        public ContestMethods Contest { get; private set; }
        public ProblemSetMethods ProblemSet { get; private set; }
        public UserMethods User { get; private set; }

        public Langs Lang { get; set; }
        public ApiKey ApiKey { get; set; }

        public Codeforces(Langs lang = Langs.EN, ApiKey apiKey = null)
        {
            BlogEntry = MethodsGroup.Init<BlogEntryMethods>(this);
            Contest = MethodsGroup.Init<ContestMethods>(this);
            ProblemSet = MethodsGroup.Init<ProblemSetMethods>(this);
            User = MethodsGroup.Init<UserMethods>(this);

            Lang = lang;
            ApiKey = apiKey;
        }
        protected virtual void AddParameters(string method, Dictionary<string, object> parameters)
        {
            parameters["lang"] = Lang.ToString().ToLower();
            if (ApiKey != null)
            {
                ApiKey.AddSignature(method, parameters);
            }
        }

        /// <summary>
        /// Returns recent actions.
        /// </summary>
        /// <param name="maxCount">Number of recent actions to return. Can be up to 100.</param>
        /// <returns>Returns a list of RecentAction objects.</returns>
        public async Task<RecentAction[]> RecentActions(int maxCount)
        {
            return await SendWebRequest<RecentAction[]>("recentActions", new Dictionary<string, object>
            {
                { "maxCount", maxCount }
            });
        }

        /// <summary>
        /// Call codeforces API.
        /// </summary>
        /// <typeparam name="T">Requeried object</typeparam>
        /// <param name="method">Method name</param>
        /// <param name="parameters">Method arguments</param>
        /// <returns></returns>
        public async Task<T> SendWebRequest<T>(string method, Dictionary<string, object> parameters = null)
        {
            if (parameters == null) parameters = new Dictionary<string, object>();
            
            AddParameters(method, parameters);
            
            var req = UrlHelper.ToGetRequest(parameters);
            //var uri = ApiUrl + method; // POST
            var uri = ApiUrl + method + "?" + req;

            using (var client = new HttpClient())
            {
                ApiResponse<T> responseObject = null;
                try
                {
                    /*
                    var jsonResp = await (await client.PostAsync(uri, new FormUrlEncodedContent(
                        parameters.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString())))
                        )).Content.ReadAsStringAsync(); // POST
                    */
                    var jsonResp = await client.GetStringAsync(uri);
                    responseObject = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonResp);      
                }
                catch (HttpRequestException)
                {
                }
                if (responseObject == null)
                    throw new Exception("No response received");
                if (responseObject.Status != ApiResponseStatus.OK )
                    throw new Exception(responseObject.Comment);

                return responseObject.Result;
            }
        }
    }
}
