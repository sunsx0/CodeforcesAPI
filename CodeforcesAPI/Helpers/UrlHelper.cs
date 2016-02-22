using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CodeforcesAPI.Helpers
{
    public static class UrlHelper
    {
        public static string UrlEncode(KeyValuePair<string, object> parameter)
        {
            return string.Format("{0}={1}",
                HttpUtility.UrlEncode(parameter.Key),
                HttpUtility.UrlEncode(parameter.Value.ToString()));
        }
        public static string ToGetRequest(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return string.Join("&", parameters.Select(x => UrlHelper.UrlEncode(x)));
        }
    }
}
