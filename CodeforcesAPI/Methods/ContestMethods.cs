using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI.Methods
{
    public class ContestMethods : MethodsGroup
    {
        public override string GroupName
        {
            get
            {
                return "contest";
            }
        }

        /// <summary>
        /// Returns list of hacks in the specified contests. Full information about hacks is available only after some time after the contest end. During the contest user can see only own hacks.
        /// </summary>
        /// <param name="contestId">Id of the contest. It is not the round number. It can be seen in contest URL. For example: /contest/566/status</param>
        /// <returns>Returns a list of Hack objects.</returns>
        public async Task<Hack[]> Hacks(int contestId)
        {
            return await SendWebRequest<Hack[]>("hacks", new Dictionary<string, object>
            {
                { "contestId", contestId }
            });
        }
        /// <summary>
        /// Returns information about all available contests.
        /// </summary>
        /// <param name="gym">If true — than gym contests are returned. Otherwide, regular contests are returned.</param>
        /// <returns>Returns a list of Contest objects. If this method is called not anonymously, then all available contests for a calling user will be returned too, including mashups and private gyms.</returns>
        public async Task<Contest[]> List(bool gym = false)
        {
            return await SendWebRequest<Contest[]>("list", new Dictionary<string, object>
            {
                { "gym", gym.ToString().ToLower() }
            });
        }
        /// <summary>
        /// Returns rating changes after the contest.
        /// </summary>
        /// <param name="contestId">Id of the contest. It is not the round number. It can be seen in contest URL. For example: /contest/566/status</param>
        /// <returns>Returns a list of RatingChange objects.</returns>
        public async Task<RatingChange[]> RatingChanges(int contestId)
        {
            return await SendWebRequest<RatingChange[]>("ratingChanges", new Dictionary<string, object>
            {
                { "contestId", contestId }
            });
        }
        /* 
         * Don't work now :(
         * 
        /// <summary>
        /// Returns list of contest-registrants.
        /// </summary>
        /// <param name="contestId">Id of the contest. It is not the round number. It can be seen in contest URL. For example: /contest/566/status</param>
        /// <returns>Returns list of contest-registrants.</returns>
        public async Task<ContestRegistrant[]> Registrants(int contestId)
        {
            return await SendWebRequest<ContestRegistrant[]>("registrants", new Dictionary<string, object>
            {
                { "contestId", contestId }
            });
        }
        */
        /// <summary>
        /// Returns the description of the contest and the requested part of the standings.
        /// </summary>
        /// <param name="contestId">Id of the contest. It is not the round number. It can be seen in contest URL. For example: /contest/566/status</param>
        /// <param name="from">1-based index of the standings row to start the ranklist.</param>
        /// <param name="count">Number of standing rows to return.</param>
        /// <param name="room">If specified, than only participants from this room will be shown in the result. If not — all the participants will be shown.</param>
        /// <param name="showUnofficial">If true than all participants (virtual, out of competition) are shown. Otherwise, only official contestants are shown.</param>
        /// <param name="handles">Semicolon-separated list of handles. No more than 10000 handles is accepted.</param>
        /// <returns>Returns object with three fields: "contest", "problems" and "rows". Field "contest" contains a Contest object. Field "problems" contains a list of Problem objects. Field "rows" contains a list of RanklistRow objects.</returns>
        public async Task<ContestStandings> Standings(int contestId, int from = 1, int count = -1, int room = -1, bool showUnofficial = false, params string[] handles)
        {
            var p = new Dictionary<string, object>
            {
                { "contestId", contestId }
            };
            if (from  != 1)  p["from"]  = from;
            if (count != -1) p["count"] = count;
            if (room  != -1) p["room"]  = room;

            p["showUnofficial"] = showUnofficial.ToString().ToLower();
            
            if (handles.Length > 0)
            {
                p["handles"] = string.Join(";", handles);
            }

            return await SendWebRequest<ContestStandings>("standings", p);
        }
        /// <summary>
        /// Returns submissions for specified contest. Optionally can return submissions of specified user.
        /// </summary>
        /// <param name="contestId">Id of the contest. It is not the round number. It can be seen in contest URL. For example: /contest/566/status</param>
        /// <param name="handle">Codeforces user handle.</param>
        /// <param name="from">1-based index of the first submission to return.</param>
        /// <param name="count">Number of returned submissions.</param>
        /// <returns>Returns a list of Submission objects, sorted in decreasing order of submission id.</returns>
        public async Task<Submission[]> Status(int contestId, string handle = null, int from = 1, int count = -1)
        {
            var p = new Dictionary<string, object>
            {
                { "contestId", contestId }
            };
            if (handle != null) p["handle"] = handle;
            if (from   != 1)    p["from"]   = from;
            if (count  != -1)   p["count"]  = count;

            return await SendWebRequest<Submission[]>("status", p);
        }
    }
}
