using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CodeforcesAPI;
using CodeforcesAPI.Utils;

namespace RatingCalculation
{
    class Program
    {

        static void Main(string[] args)
        {
            var api = new Codeforces();
            var calc = new CodeforcesRatingCalculator2();

            Console.Write("Enter contest id: ");
            var contestId = int.Parse(Console.ReadLine().Trim());

            var standings = api.Contest.Standings(contestId, showUnofficial: false).Result;
            var registrants = api.Contest.Registrants(contestId).Result;

            Console.WriteLine("Standings rows: " + standings.Rows.Length);
            Console.WriteLine("Registrants rows: " + registrants.Length);


            var magicRating = new Dictionary<string, int>();
            foreach (var registrant in registrants)
            {
                foreach (var member in registrant.Party.Members)
                {
                    magicRating[member.Handle] = registrant.Rating;
                }
            }
            var previousRating = new Dictionary<Party, int>();
            foreach (var row in standings.Rows)
            {
                int rating;
                magicRating.TryGetValue(row.Party.Members.First().Handle, out rating);
                if (rating == 0) rating = 1500;
                previousRating[row.Party] = rating;
            }
            var sw = Stopwatch.StartNew();
            var calculated = calc.CalculateRatingChanges(previousRating, standings.Rows, null);
            sw.Stop();

            Console.WriteLine("Calculated after {0} ms", sw.ElapsedMilliseconds);
            using (var file = File.CreateText("result.txt"))
            {
                foreach (var changeRow in calculated.OrderBy(x => -x.Value))
                {
                    var name = string.Format("{0} ({1})",
                        changeRow.Key.TeamName,
                        string.Join(", ", changeRow.Key.Members.Select(x => x.Handle)));

                    file.WriteLine("{0} : {1}", name, changeRow.Value);
                }
            }
            Console.WriteLine("Saved to result.txt");
        }
    }
}
