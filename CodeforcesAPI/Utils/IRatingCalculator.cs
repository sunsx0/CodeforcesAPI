using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI.Utils
{
    public interface IRatingCalculator
    {
        int AggregateRating(RatingChange[] ratingChanges);
        int GetMaxRating(RatingChange[] ratingChanges);
        Dictionary<Party, int> CalculateRatingChanges(Dictionary<Party, int> previousRatings, RanklistRow[] standingsRows, HashSet<Party> newcomers);
    }
}
