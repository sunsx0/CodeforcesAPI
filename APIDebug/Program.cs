using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CodeforcesAPI;
using System.Threading.Tasks;

namespace APIDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new Codeforces(Langs.EN);
            foreach (var user in api.User.RatedList(true).Result.
                Where(x => x.MaxRating > 2600).
                OrderBy(x => x.MaxRating).
                Reverse())
                Console.WriteLine("{0} - {1} ({2})", user.Handle, user.MaxRating, user.MaxRank);
        }
    }
}
