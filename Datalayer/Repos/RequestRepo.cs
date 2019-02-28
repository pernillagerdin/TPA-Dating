using Datalayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Datalayer.Repos {
    public class RequestRepo : Repo<RequestModels, int> {
        public RequestRepo(ApplicationDbContext context) : base(context) { }

        public List<RequestModels> GetSentRequests(string profileID) {
            return Items.Where(request => request.RequestFromID.Equals(profileID)).ToList();
        }

        public List<RequestModels> GetRequests(string profileID) {
            return Items.Where(request => request.RequestToID.Equals(profileID)).ToList();
        }

        public bool SentRequestPending(string currentProfileID, string profileID) {
            return Items.Any(request => request.RequestFromID.Equals(currentProfileID) && request.RequestToID.Equals(profileID));
        }

        public bool RequestPending(string currentProfileID, string profileID) {
            return Items.Any(request => request.RequestToID.Equals(currentProfileID) && request.RequestFromID.Equals(profileID));
        }
    }
}
