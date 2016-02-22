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
        static void Main()
        {
            var api = new Codeforces(Langs.EN);
            var users = api.User.RatedList(true).Result;

            Console.WriteLine(api.User.RatedList(true).Result.Count());
        }
    }
}
