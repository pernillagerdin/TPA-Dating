using Datalayer.Models;
using Datalayer.Repos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TPA_DatingMVC.Controllers {
    [Authorize(Roles ="hasProfile")]
    public class ContactApiController : ApiController {
        
        private ProfileRepo profileRepo;
        private RequestRepo requestRepo;
        private ContactRepo contactRepo;

        public ContactApiController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepo = new ProfileRepo(context);
            requestRepo = new RequestRepo(context);
            contactRepo = new ContactRepo(context);
        }

        [HttpPost]
        public void SendRequest(string id) {
            RequestModels request = new RequestModels {
                RequestFromID = User.Identity.GetUserId(),
                RequestToID = id,
                RequestTimeStamp = DateTime.Now
            };
            requestRepo.Add(request);
            requestRepo.Save();
        }

        [HttpPost]
        public void CancelRequest(string id) {
            if(requestRepo.SentRequestPending(User.Identity.GetUserId(), id)) {
                List<RequestModels> sentRequest = requestRepo.GetSentRequests(User.Identity.GetUserId());
                foreach(RequestModels request in sentRequest) {
                    if(request.RequestFromID.Equals(User.Identity.GetUserId()) && request.RequestToID.Equals(id)) {
                        requestRepo.Remove(request.RequestID);
                    }
                }
                requestRepo.Save();
             }
        }

        [HttpPost]
        public void AcceptRequest(string id) {
            if(requestRepo.RequestPending(User.Identity.GetUserId(), id)) {
                List<RequestModels> myRequests = requestRepo.GetRequests(User.Identity.GetUserId());
                foreach(RequestModels request in myRequests) {
                    if (request.RequestFromID.Equals(id) && request.RequestToID.Equals(User.Identity.GetUserId())){
                        requestRepo.Remove(request.RequestID);
                        ContactModels contact = new ContactModels {
                            ContactAID = User.Identity.GetUserId(),
                            ContactBID = id
                        };
                        contactRepo.Add(contact);
                    }
                }
                requestRepo.Save();
                contactRepo.Save();
            }
        }

        [HttpPost]
        public void DeclineRequest(string id) {
            if(requestRepo.RequestPending(User.Identity.GetUserId(), id)){
                List<RequestModels> myRequests = requestRepo.GetRequests(User.Identity.GetUserId());
                foreach(RequestModels request in myRequests) {
                    if(request.RequestFromID.Equals(id) && request.RequestToID.Equals(User.Identity.GetUserId())) {
                        requestRepo.Remove(request.RequestID);
                    }
                }
                requestRepo.Save();
            }
        }

    }
}
