using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI.Utils
{
    /*
     * Copyright by Mike Mirzayanov
     * @author Mike Mirzayanov (mirzayanovmr@gmail.com)
     */
    public class CodeforcesRatingCalculator2 : IRatingCalculator
    {
        public const int InitialRating = 1500;
        public int AggregateRating(RatingChange[] ratingChanges)
        {
            int rating = InitialRating;

            if (ratingChanges != null)
            {
                foreach (var ratingChange in ratingChanges)
                {
                    rating += ratingChange.NewRating - ratingChange.OldRating;
                }
            }

            return rating;

        }

        public Dictionary<Party, int> CalculateRatingChanges(Dictionary<Party, int> previousRatings, RanklistRow[] standingsRows, HashSet<Party> newcomers)
        {
            List<Contestant> contestants = new List<Contestant>(standingsRows.Length);

            foreach (var standingsRow in standingsRows)
            {
                int rank = standingsRow.Rank;
                var party = standingsRow.Party;
                contestants.Add(new Contestant(party, rank, standingsRow.Points, previousRatings[party]));
            }

            Process(contestants);

            Dictionary<Party, int> ratingChanges = new Dictionary<Party, int>();
            foreach (var contestant in contestants)
            {
                ratingChanges.Add(contestant.Party, contestant.Delta);
            }

            return ratingChanges;
        }

        public int GetMaxRating(RatingChange[] ratingChanges)
        {
            var maxRating = 0;

            if (ratingChanges != null)
            {
                int rating = InitialRating;
                foreach (var ratingChange in ratingChanges)
                {
                    rating += ratingChange.NewRating - ratingChange.OldRating;
                    maxRating = Math.Max(rating, maxRating);
                }
            }

            return maxRating;
        }
        //  1.0 / (1.0 + pow((long double) 10.0, (rb - ra) / 400.0));
        private static double GetEloWinProbability(double ra, double rb)
        {
            return 1.0 / (1 + Math.Pow(10, (rb - ra) / 400.0));
        }
        private double GetEloWinProbability(Contestant a, Contestant b)
        {
            return GetEloWinProbability(a.Rating, b.Rating);
        }

        public int ComposeRatingsByTeamMemberRatings(int[] ratings)
        {
            double left = 100;
            double right = 4000;

            for (int tt = 0; tt < 20; tt++)
            {
                double r = (left + right) / 2.0;

                double rWinsProbability = 1.0;
                foreach (var rat in ratings)
                {
                    rWinsProbability *= GetEloWinProbability(r, rat);
                }

                double rating = Math.Log10(1 / (rWinsProbability) - 1) * 400 + r;

                if (rating > r)
                {
                    left = r;
                }
                else {
                    right = r;
                }
            }

            return (int)Math.Round((left + right) / 2);
        }

        private double GetSeed(List<Contestant> contestants, Contestant contestant, int rating)
        {
            Contestant extraContestant = new Contestant(null, 0, 0, rating);

            double result = 1;
            foreach (var other in contestants)
            {
                if (other != contestant)
                {
                    result += GetEloWinProbability(other, extraContestant);
                }
            }

            return result;
        }
        private int GetRatingToRank(List<Contestant> contestants, Contestant contestant, double rank)
        {
            int left = 1;
            int right = 8000;

            while (right - left > 1)
            {
                int mid = (left + right) / 2;

                if (GetSeed(contestants, contestant, mid) < rank)
                {
                    right = mid;
                }
                else
                {
                    left = mid;
                }
            }

            return left;
        }
        private void ReassignRanks(List<Contestant> contestants)
        {
            SortByPointsDesc(contestants);

            foreach (var contestant in contestants)
            {
                contestant.Rank = 0;
                contestant.Delta = 0;
            }

            int first = 0;
            double points = contestants[0].Points;
            for (int i = 1; i < contestants.Count; i++)
            {
                if (contestants[i].Points < points)
                {
                    for (int j = first; j < i; j++)
                    {
                        contestants[j].Rank = i;
                    }
                    first = i;
                    points = contestants[i].Points;
                }
            }

            {
                double rank = contestants.Count;
                for (int j = first; j < contestants.Count; j++)
                {
                    contestants[j].Rank = rank;
                }
            }
        }
        private void SortByPointsDesc(List<Contestant> contestants)
        {
            contestants.Sort((a, b) => -(a.Points.CompareTo(b.Points)));
        }

        private void Process(List<Contestant> contestants)
        {
            if (contestants == null || contestants.Count == 0)
            {
                return;
            }

            ReassignRanks(contestants);

            foreach (var a in contestants)
            {
                a.Seed = 1;
                foreach (var b in contestants)
                {
                    if (a != b)
                    {
                        a.Seed += GetEloWinProbability(b, a);
                    }
                }
            }

            foreach (var contestant in contestants)
            {
                double midRank = Math.Sqrt(contestant.Rank * contestant.Seed);
                contestant.NeedRating = GetRatingToRank(contestants, contestant, midRank);
                contestant.Delta = (contestant.NeedRating - contestant.Rating) / 2;
            }

            SortByRatingDesc(contestants);

            // Total sum should not be more than zero.
            {
                int sum = 0;
                foreach (var c in contestants)
                {
                    sum += c.Delta;
                }
                int inc = -sum / contestants.Count - 1;
                foreach (var contestant in contestants)
                {
                    contestant.Delta += inc;
                }
            }

            // Sum of top-4*sqrt should be adjusted to zero.
            {
                int sum = 0;
                int zeroSumCount = Math.Min((int)(4 * Math.Round(Math.Sqrt(contestants.Count))), contestants.Count);
                for (int i = 0; i < zeroSumCount; i++)
                {
                    sum += contestants[i].Delta;
                }
                int inc = Math.Min(Math.Max(-sum / zeroSumCount, -10), 0);
                foreach (var contestant in contestants)
                {
                    contestant.Delta += inc;
                }
            }

            ValidateDeltas(contestants);
        }
        private void ValidateDeltas(List<Contestant> contestants)
        {
            SortByPointsDesc(contestants);

            for (int i = 0; i < contestants.Count; i++)
            {
                for (int j = i + 1; j < contestants.Count; j++)
                {
                    if (contestants[i].Rating > contestants[j].Rating)
                    {
                        Ensure(contestants[i].Rating + contestants[i].Delta >= contestants[j].Rating + contestants[j].Delta,
                                "First rating invariant failed: " + contestants[i].Party + " vs. " + contestants[j].Party + ".");
                    }
                    if (contestants[i].Rating < contestants[j].Rating)
                    {
                        if (contestants[i].Delta < contestants[j].Delta)
                        {
                            Console.WriteLine(1);
                        }
                        Ensure(contestants[i].Delta >= contestants[j].Delta,
                                "Second rating invariant failed: " + contestants[i].Party + " vs. " + contestants[j].Party + ".");
                    }
                }
            }
        }

        private void Ensure(bool b, string message)
        {
            if (!b)
            {
                throw new Exception(message);
            }
        }

        private void SortByRatingDesc(List<Contestant> contestants)
        {
            contestants.Sort((a, b) => -(a.Rating.CompareTo(b.Rating)));
        }
    }
}