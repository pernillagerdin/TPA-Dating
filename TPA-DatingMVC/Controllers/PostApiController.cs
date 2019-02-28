using Datalayer.Models;
using Datalayer.Repos;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace TPA_DatingMVC.Controllers {
    [Authorize(Roles ="hasProfile")]
    public class PostApiController : ApiController {
        private PostRepo postRepo;

        public PostApiController() {
            ApplicationDbContext context = new ApplicationDbContext();
            postRepo = new PostRepo(context);
        }

        [HttpPost]
        public void AddPost(PostModels model) {
            if (ModelState.IsValid) {
                PostModels post = new PostModels {
                    PostFromID = User.Identity.GetUserId(),
                    PostToID = model.PostToID,
                    PostContent = model.PostContent,
                    PostTimeStamp = DateTime.Now
                };
                postRepo.Add(post);
                postRepo.Save();
            }
        }
    }
}
