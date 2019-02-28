using Datalayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Datalayer.Repos {
    public class PostRepo : Repo<PostModels, int> {
        public PostRepo(ApplicationDbContext context) : base(context) { }
        public List<PostModels> GetPostsForProfile(string profileID) {
            return Items.Where(post => post.PostToID.Equals(profileID)).OrderByDescending(post => post.PostTimeStamp).ToList();
        }
    }
}
