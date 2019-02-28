using Datalayer.Models;
using Datalayer.Repos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TPA_DatingMVC.Controllers {
    [Authorize]
    public class NotificationsController : Controller {

        // GET: Notifications
        private ProfileRepo profileRepo;
        private RequestRepo requestRepo;

        public NotificationsController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepo = new ProfileRepo(context);
            requestRepo = new RequestRepo(context);
        }

        [HttpPost]
        public ActionResult GetNumber() {
            if (!profileRepo.IfProfileExists(User.Identity.GetUserId())) {
                return Json(new { Number = 0 });
            }
            List<RequestModels> requests = requestRepo.GetRequests(User.Identity.GetUserId());
            return Json(new { Number = requests.Count });
        }

        [HttpPost]
        public PartialViewResult GetContent() {
            if (!profileRepo.IfProfileExists(User.Identity.GetUserId())) {
                return PartialView("_NoProfile");
            }
            List<RequestModels> requests = requestRepo.GetRequests(User.Identity.GetUserId());
            if (requests.Count >= 1) {
                List<RequestModels> updatedRequests = requests.Select(request => new RequestModels {
                    RequestID = request.RequestID,
                    RequestFromID = request.RequestFromID,
                    RequestFrom = profileRepo.Get(request.RequestFromID),
                    RequestToID = request.RequestToID,
                    RequestTimeStamp = request.RequestTimeStamp

                }).ToList();
                return PartialView("_Notifications", updatedRequests);
            }
            return PartialView("_NoNotifications");
        }


    }
}