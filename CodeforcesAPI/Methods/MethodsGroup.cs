using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI.Methods
{
    public abstract class MethodsGroup
    {
        public Codeforces Api { get; private set; }
        public abstract string GroupName { get; }
        protected async Task<T> SendWebRequest<T>(string method, Dictionary<string, object> parameters = null)
        {
            return await Api.SendWebRequest<T>(GroupName + "." + method, parameters);
        }

        public static T Init<T>(Codeforces api) where T : MethodsGroup, new()
        {
            return new T
            {
                Api = api
            };
        }
    }
}
