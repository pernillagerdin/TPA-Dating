using Datalayer.Models;
using Datalayer.Repos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TPA_DatingMVC.Controllers {
    public class HomeController : Controller {
        private ProfileRepo profileRepo;
        private Random random;

        public HomeController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepo = new ProfileRepo(context);
            random = new Random();
        }

        public ActionResult Index() {
            List<ProfileModels> allProfiles = profileRepo.GetAll();
            List<ProfileModels> randomProfiles = new List<ProfileModels>();
            for (int i = 0; i < 3; i++) {
                ProfileModels specificProfile = allProfiles[random.Next(allProfiles.Count)];
                if (!randomProfiles.Exists(profile => profile.Equals(specificProfile))) {
                    randomProfiles.Add(specificProfile);
                    continue;
                }
                else {
                    i--;
                }
            }
            return View(randomProfiles);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}