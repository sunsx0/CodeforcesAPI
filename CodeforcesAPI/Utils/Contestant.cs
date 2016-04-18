using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI.Utils
{
    public class Contestant
    {
        public Party Party { get; set; }
        public double Rank { get; set; }
        public double Points { get; set; }
        public int Rating { get; set; }
        public int NeedRating { get; set; }
        public double Seed { get; set; }
        public int Delta { get; set; }

        public Contestant(Party party, int rank, double points, int rating)
        {
            this.Party = party;
            this.Rank = rank;
            this.Points = points;
            this.Rating = rating;
        }
    }
}
