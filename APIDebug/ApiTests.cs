using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeforcesAPI;
using CodeforcesAPI.Utils;

namespace APIDebug
{
    delegate void TestHandler(Codeforces api);
    static class ApiTests
    {
        public static void RecentActionsTest1(Codeforces api)
        {
            foreach(var x in api.RecentActions(10).Result)
            {
                if (x.Comment != null)
                {
                    Console.WriteLine("[comment][{0}] {1}: {2}", x.Time, x.Comment.CommentatorHandle, x.Comment.Text);
                }
                if (x.BlogEntry != null)
                {
                    Console.WriteLine("[blog][{0}] {1}", x.Time, x.BlogEntry.Title);
                }
            }
        }
        public static void BlogEntryView1(Codeforces api)
        {
            var view = api.BlogEntry.View(44362).Result;
            Console.WriteLine(view.Title);
            Console.WriteLine("Rating: " + view.Rating);
            Console.WriteLine("Author: " + view.AuthorHandle);
        }
        public static void BlogEntryComments1(Codeforces api)
        {
            var view = api.BlogEntry.Comments(44362).Result;
            Console.WriteLine("Comments: " + view.Length);
            foreach(var x in view)
            {
                Console.WriteLine("{0}: {1}", x.CommentatorHandle, x.Text);
            }
        }
    }
}
