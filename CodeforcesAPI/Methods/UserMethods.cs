using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI.Methods
{
    public class UserMethods : MethodsGroup
    {
        public override string GroupName
        {
            get
            {
                return "user";
            }
        }

        /// <summary>
        /// Returns information about one or several users.
        /// </summary>
        /// <param name="handles">Semicolon-separated list of handles. No more than 10000 handles is accepted.</param>
        /// <returns>Returns a list of User objects for requested handles.</returns>
        public async Task<User[]> Info(params string[] handles)
        {
            return await SendWebRequest<User[]>("info", new Dictionary<string, object>
            {
                { "handles", string.Join(";", handles) }
            });
        }
        /// <summary>
        /// Returns the list users who have participated in at least one rated contest.
        /// </summary>
        /// <param name="activeOnly">If true then only users, who participated in rated contest during the last month are returned. Otherwise, all users with at least one rated contest are returned.</param>
        /// <returns>Returns a list of User objects, sorted in decreasing order of rating.</returns>
        public async Task<User[]> RatedList(bool activeOnly = false)
        {
            return await SendWebRequest<User[]>("ratedList", new Dictionary<string, object>
            {
                { "activeOnly", activeOnly.ToString().ToLower() }
            });
        }
        /// <summary>
        /// Returns rating history of the specified user.
        /// </summary>
        /// <param name="handle">Codeforces user handle.</param>
        /// <returns>Returns a list of RatingChange objects for requested user.</returns>
        public async Task<RatingChange[]> Rating(string handle)
        {
            return await SendWebRequest<RatingChange[]>("rating", new Dictionary<string, object>
            {
                { "handle", handle }
            });
        }
        /// <summary>
        /// Returns submissions of specified user.
        /// </summary>
        /// <param name="handle">Codeforces user handle.</param>
        /// <param name="from">1-based index of the first submission to return.</param>
        /// <param name="count">Number of returned submissions.</param>
        /// <returns>Returns a list of Submission objects, sorted in decreasing order of submission id.</returns>
        public async Task<Submission[]> Status(string handle, int from = 1, int count = -1)
        {
            var p = new Dictionary<string, object>
            {
                { "handle", handle }
            };

            if (from  != 1)  p["from"]  = from;
            if (count != -1) p["count"] = count;

            return await SendWebRequest<Submission[]>("status", p);
        }
    }
}
