using Datalayer.Models;
using Datalayer.Repos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPA_DatingMVC.Models;

namespace TPA_DatingMVC.Controllers {
    [Authorize]
    public class ProfileController : Controller {
        private ProfileRepo profileRepo;
        private UserRepo userRepo;
        private PostRepo postRepo;
        private RequestRepo requestRepo;
        private ContactRepo contactRepo;

        public ProfileController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepo = new ProfileRepo(context);
            userRepo = new UserRepo(context);
            postRepo = new PostRepo(context);
            requestRepo = new RequestRepo(context);
            contactRepo = new ContactRepo(context);



        }

        // GET: Profile

        [Authorize(Roles = "hasProfile")]
        public ActionResult Index() {
            string currentUser = User.Identity.GetUserId();
            object profileID = Request.RequestContext.RouteData.Values["id"];
            ProfileModels profile = profileRepo.Get(currentUser);

            if (!string.IsNullOrWhiteSpace((string)profileID)) {

                profile = profileRepo.Get((string)profileID);
            }

            string relation = "None";
            if (contactRepo.Contacts(currentUser, (string)profileID)) {
                relation = "Contacts";
            }
            else if (requestRepo.RequestPending(currentUser, (string)profileID)) {
                relation = "IncomingRequest";
            }
            else if (requestRepo.SentRequestPending(currentUser, (string)profileID)) {
                relation = "OutgoingRequest";
            }


            PostViewModel postViewModel = ConvertPostToViewModel(postRepo.GetPostsForProfile(profile.ProfileID));
            ContactViewModel contactViewModel = ConvertContactToViewModel(contactRepo.GetContacts((string)profileID), (string)profileID);
            ProfileViewModel profileViewModel = new ProfileViewModel {
                CurrentUser = currentUser,
                ProfileID = profile.ProfileID,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Bio = profile.Bio,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                ProfilePicture = profile.ProfilePicture,
                Posts = postViewModel,
                Relation = relation,
                Contacts = contactViewModel

            };
            return View(profileViewModel);
        }



        public ActionResult CreateProfile() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateProfile([Bind(Exclude = "ProfilePicture")]ProfileModels profile) {
            if (!ModelState.IsValid) { return RedirectToAction("CreateProfile"); }
            if (Request.Files["ProfilePicture"].ContentLength >= 1) {
                HttpPostedFileBase profileImg = Request.Files["ProfilePicture"];
                using (BinaryReader binary = new BinaryReader(profileImg.InputStream)) {
                    profile.ProfilePicture = binary.ReadBytes(profileImg.ContentLength);
                }
            }
            else {
                string path = AppDomain.CurrentDomain.BaseDirectory + "/Content/Images/defaultProfile.png";
                using (FileStream file = new FileStream(path, FileMode.Open)) {
                    using (BinaryReader binary = new BinaryReader(file)) {
                        profile.ProfilePicture = binary.ReadBytes((int)file.Length);
                    }
                }

            }
            //Things needed but not in form
            profile.ProfileID = User.Identity.GetUserId();
            profileRepo.Add(profile);
            profileRepo.Save();

            //Adds user to role
            userRepo.AddUserToRole(User.Identity.GetUserId(), "hasProfile");

            //Set displayname
            ApplicationUser user = userRepo.Get(User.Identity.GetUserId());
            user.DisplayName = (profile.FirstName + " " + profile.LastName);
            userRepo.Edit(user);
            userRepo.Save();
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "hasProfile")]
        public ActionResult EditProfile() { return View(); }

        [Authorize(Roles = "hasProfile")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditProfile([Bind(Exclude = "ProfilePicture")] ProfileModels updates) {
            if (!ModelState.IsValid) { return RedirectToAction("EditProfile"); }

            ProfileModels activeProfile = profileRepo.Get(User.Identity.GetUserId());

            activeProfile.Gender = updates.Gender;
            activeProfile.Bio = updates.Bio;
            if (Request.Files["ProfilePicture"].ContentLength >= 1) {
                HttpPostedFileBase profileImg = Request.Files["ProfilePicture"];
                using (BinaryReader binary = new BinaryReader(profileImg.InputStream)) {
                    activeProfile.ProfilePicture = binary.ReadBytes(profileImg.ContentLength);
                }
            }

            if (activeProfile.FirstName.Equals(updates.FirstName) && activeProfile.LastName.Equals(updates.LastName)) {
                profileRepo.Edit(activeProfile);
                profileRepo.Save();
                return RedirectToAction("Index");
            }

            activeProfile.FirstName = updates.FirstName;
            activeProfile.LastName = updates.LastName;
            profileRepo.Edit(activeProfile);
            profileRepo.Save();
            ApplicationUser user = userRepo.Get(User.Identity.GetUserId());
            user.DisplayName = (activeProfile.FirstName + " " + activeProfile.LastName);
            userRepo.Edit(user);
            userRepo.Save();

            return RedirectToAction("Login", "Account");

        }
        [Authorize(Roles = "hasProfile")]
        public PartialViewResult UpdatePostWall(string id) {
            PostViewModel model = ConvertPostToViewModel(postRepo.GetPostsForProfile(id));
            return PartialView("_PostWall", model);
        }

        [Authorize(Roles = "hasProfile")]
        public PartialViewResult UpdateContacts(string id) {
            ContactViewModel model = ConvertContactToViewModel(contactRepo.GetContacts(id), id);
            return PartialView("_Contacts", model);
        }


        [AllowAnonymous]
        public FileContentResult RenderProfilePicture(string id) {
            var profileID = Request.RequestContext.RouteData.Values["id"];
            ProfileModels profile = null;
            if (!string.IsNullOrWhiteSpace(id)) {
                profile = profileRepo.Get(id);
            }
            else {
                profile = profileRepo.Get((string)profileID);
            }
            return new FileContentResult(profile.ProfilePicture, "image/jpeg");


        }

        [Authorize(Roles = "hasProfile")]
        public PostViewModel ConvertPostToViewModel(List<PostModels> posts) {
            List<PostModels> updatedPosts = posts.Select(post => new PostModels {
                PostID = post.PostID,
                PostFromID = post.PostFromID,
                PostToID = post.PostToID,
                PostContent = post.PostContent,
                PostTimeStamp = post.PostTimeStamp,
                PostFrom = profileRepo.Get(post.PostFromID)
            }).ToList();
            PostViewModel model = new PostViewModel {
                Posts = updatedPosts
            };
            return model;
        }
        [Authorize(Roles = "hasProfile")]
        public ContactViewModel ConvertContactToViewModel(List<ContactModels> contacts, string profileID) {
            List<ContactModels> updatedContacts = contacts.Select(contact => new ContactModels {
                ContactID = contact.ContactID,
                ContactAID = contact.ContactAID,
                ContactA = profileRepo.Get(contact.ContactAID),
                ContactBID = contact.ContactBID,
                ContactB = profileRepo.Get(contact.ContactBID)
            }).ToList();
            ContactViewModel model = new ContactViewModel { Contacts = updatedContacts, CurrentProfileID = profileID };
            return model;

        }
    }
}