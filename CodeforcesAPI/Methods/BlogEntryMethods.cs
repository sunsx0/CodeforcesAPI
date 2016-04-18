using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesAPI.Methods
{
    public class BlogEntryMethods : MethodsGroup
    {
        public override string GroupName
        {
            get
            {
                return "blogEntry";
            }
        }
        /// <summary>
        /// Returns a list of comments to the specified blog entry.
        /// </summary>
        /// <param name="blogEntryId">Id of the blog entry. It can be seen in blog entry URL. For example: /blog/entry/79</param>
        /// <returns>A list of Comment objects.</returns>
        public async Task<Comment[]> Comments(int blogEntryId)
        {
            return await SendWebRequest<Comment[]>("comments", new Dictionary<string, object>
            {
                { "blogEntryId", blogEntryId }
            });
        }
        /// <summary>
        /// Returns blog entry.
        /// </summary>
        /// <param name="blogEntryId">Id of the blog entry. It can be seen in blog entry URL. For example: /blog/entry/79</param>
        /// <returns>Returns a BlogEntry object in full version.</returns>
        public async Task<BlogEntry> View(int blogEntryId)
        {
            return await SendWebRequest<BlogEntry>("view", new Dictionary<string, object>
            {
                { "blogEntryId", blogEntryId }
            });
        }
    }
}
