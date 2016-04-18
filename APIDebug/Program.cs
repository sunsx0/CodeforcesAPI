using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CodeforcesAPI.Utils;
using CodeforcesAPI;
using System.Diagnostics;
using System.Threading.Tasks;

namespace APIDebug
{
    class Program
    {
        static Dictionary<string, TestHandler> Tests = new Dictionary<string, TestHandler>
        {
            { "recentActions(10)", ApiTests.RecentActionsTest1 },
            { "blogView1", ApiTests.BlogEntryView1 },
            { "blogComments1", ApiTests.BlogEntryComments1 }
        };
        static void Main(string[] args)
        {
            var api = new Codeforces();
            var sw = new Stopwatch();
            foreach(var test in Tests)
            {
                Console.WriteLine(test.Key + ":");

                sw.Restart();
                test.Value(api);
                sw.Stop();

                Console.WriteLine("{0} complete after {1} ms", test.Key, sw.ElapsedMilliseconds);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }
        }
    }
}
