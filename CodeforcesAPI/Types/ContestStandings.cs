using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI
{
    public class ContestStandings
    {
        public Contest Contest { get; set; }
        public Problem[] Problems { get; set; }
        public RanklistRow[] Rows { get; set; }
    }
}
