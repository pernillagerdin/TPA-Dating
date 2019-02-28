using Datalayer.Models;
using Datalayer.Repos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TPA_DatingMVC.Controllers {
    [Authorize(Roles = "hasProfile")]
    public class SearchController : Controller {
        private ProfileRepo profileRepo;

        public SearchController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepo = new ProfileRepo(context);
        }

        // GET: Search
        public ActionResult Index() {
            List<ProfileModels> profiles = profileRepo.GetAllExceptCurrent(User.Identity.GetUserId()).OrderBy(profile => profile.FirstName).ToList();
            return View(profiles);
        }
    }
}