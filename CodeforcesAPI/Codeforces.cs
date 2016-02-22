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

namespace CodeforcesAPI
{
    public class Codeforces
    {
        public const string ApiUrl = "http://codeforces.com/api/";

        public ContestMethods Contest { get; private set; }
        public ProblemSetMethods ProblemSet { get; private set; }
        public UserMethods User { get; private set; }

        public Langs Lang { get; set; }
        public ApiKey ApiKey { get; set; }

        public Codeforces(Langs lang = Langs.EN, ApiKey apiKey = null)
        {
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

            var req = method + "?" + UrlHelper.ToGetRequest(parameters);
            var uri = new Uri(ApiUrl + req);


            using (var client = new HttpClient())
            {
                ApiResponse<T> responseObject = null;
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                    responseObject = await response.Content.ReadAsAsync<ApiResponse<T>>().ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
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
