using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeforcesAPI.Methods;
using System.Net.Http;
using System.IO;

namespace CodeforcesAPI.Utils
{
    public static class ApiMethodsExt
    {
        const string CanMoveNext = "→</a>";
        [Obsolete]
        public static async Task<ContestRegistrant[]> Registrants(this ContestMethods contest, int contestId)
        {
            var urlPrefix = "http://codeforces.com/";
            var urlPath = string.Format("contestRegistrants/{0}/page/", contestId);

            var used = new HashSet<string>();
            var result = new List<ContestRegistrant>();
            
            var pageId = 1;
            using (var client = new HttpClient())
            {
                while (true)
                {
                    var pageContent = await client.GetStringAsync(urlPrefix + urlPath + pageId);
                    foreach (var registrant in ParseRegistrants(pageContent))
                    {
                        registrant.Party.ContestId = contestId;

                        var isUsed = false;
                        foreach(var member in registrant.Party.Members)
                        {
                            if (used.Contains(member.Handle))
                            {
                                isUsed = true;
                                break;
                            }
                            used.Add(member.Handle);
                        }
                        if (!isUsed)
                            result.Add(registrant);
                    }
                    pageId++;
                    if (!pageContent.Contains(urlPath + pageId))
                    {
                        break;
                    }
                }
            }

            return result.ToArray();
        }
        const string registrantsTableBlockBegin = "<table class=\"registrants\">";
        const string registrantsTableBlockEnd = "<script";
        const string registrantsTableHeader = "BY_RATING_DESC";
        const string registrantBlockBegin = "<tr";
        const string registrantBlockEnd = "</tr";
        private static IEnumerable<ContestRegistrant> ParseRegistrants(string pageContent)
        {
            var beginIndex = pageContent.IndexOf(registrantsTableBlockBegin);
            if (beginIndex != -1)
            {
                beginIndex += registrantsTableBlockBegin.Length;
                var endIndex = pageContent.IndexOf(registrantsTableBlockEnd, beginIndex);

                if (endIndex != -1)
                {
                    pageContent = pageContent.Substring(beginIndex, endIndex - beginIndex);

                    var pos = 0;
                    while (true)
                    {
                        beginIndex = pageContent.IndexOf(registrantBlockBegin, pos);
                        if (beginIndex == -1) break;
                        beginIndex += registrantBlockBegin.Length;
                        endIndex = pageContent.IndexOf(registrantBlockEnd, beginIndex);

                        var registrantBlock = pageContent.Substring(beginIndex, endIndex - beginIndex);
                        if (!registrantBlock.Contains(registrantsTableHeader))
                        {
                            var registrant = ParseRegistrant(registrantBlock);
                            if (registrant != null)
                                yield return registrant;
                        }

                        pos = endIndex + registrantBlockEnd.Length;
                    }
                }
            }
        }
        private static ContestRegistrant ParseRegistrant(string text)
        {
            var contestRegistrant = new ContestRegistrant();
            contestRegistrant.Party = new Party();
            var members = new List<Member>();

            contestRegistrant.Party.ParticipantType = PartyParticipantType.Contestant;
            if (text.Contains("<small>*</small>"))
            {
                contestRegistrant.Party.ParticipantType = PartyParticipantType.OutOfCompetition;
            }

            using (var reader = new StringReader(text))
            {
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("<td>") && line.Contains("</td>"))
                    {
                        var pp = line.IndexOf("<td>");
                        var indexBegin = line.IndexOf('>', pp) + 1;
                        var indexEnd = line.IndexOf('<', indexBegin);

                        if (indexBegin < indexEnd)
                        {
                            int rating;
                            var ratingStr = line.Substring(indexBegin, indexEnd - indexBegin).Trim();
                            if (int.TryParse(ratingStr, out rating))
                            {
                                contestRegistrant.Rating = rating;
                            }
                        }
                    }
                    if (line.Contains("/team"))
                    {
                        var pp = line.IndexOf("/team");
                        var indexBegin = pp + 5;
                        var indexEnd = line.IndexOf('\"', indexBegin);

                        if (indexBegin < indexEnd)
                        {
                            int id;
                            if (int.TryParse(line.Substring(indexBegin, indexEnd - indexBegin).Trim(), out id))
                            {
                                contestRegistrant.Party.TeamId = id;
                            }
                            indexBegin = line.IndexOf("=\"", pp) + 2;
                            indexEnd = line.IndexOf("\">", indexBegin);
                            if (indexBegin < indexEnd)
                            {
                                contestRegistrant.Party.TeamName = line.Substring(indexBegin, indexEnd - indexBegin);
                            }
                        }
                    }
                    {
                        var profilePointer = 0;
                        while (profilePointer < line.Length && line.IndexOf("/profile/", profilePointer) != -1)
                        {

                            var indexBegin = line.IndexOf("/profile/", profilePointer) + 9;
                            var indexEnd = line.IndexOf('\"', indexBegin);

                            if (indexBegin < indexEnd)
                            {
                                var handle = line.Substring(indexBegin, indexEnd - indexBegin);
                                members.Add(new Member
                                {
                                    Handle = handle
                                });
                                profilePointer = indexEnd + 1;
                            }
                            else { break; }
                        }
                    }
                }
            }
            if (members.Count > 0)
            {
                contestRegistrant.Party.Members = members.ToArray();
                return contestRegistrant;
            }
            else
            {
                return null;
            }
        }
    }
}
