using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI.Methods
{
    public class ProblemSetMethods : MethodsGroup
    {
        public override string GroupName
        {
            get
            {
                return "problemset";
            }
        }

        /// <summary>
        /// Returns all problems from problemset. Problems can be filtered by tags.
        /// </summary>
        /// <param name="tags">Semicilon-separated list of tags.</param>
        /// <returns>Returns two lists. List of Problem objects and list of ProblemStatistics objects.</returns>
        public async Task<ProblemSetProblems> Problems(params string[] tags)
        {
            return await SendWebRequest<ProblemSetProblems>("problems", new Dictionary<string, object>
            {
                { "handles", string.Join(";", tags) }
            });
        }
        /// <summary>
        /// Returns recent submissions.
        /// </summary>
        /// <param name="count">Number of submissions to return. Can be up to 1000</param>
        /// <returns>Returns a list of Submission objects, sorted in decreasing order of submission id.</returns>
        public async Task<Submission[]> RecentStatus(int count)
        {
            return await SendWebRequest<Submission[]>("recentStatus", new Dictionary<string, object>
            {
                { "count", count }
            });
        }
    }
}
