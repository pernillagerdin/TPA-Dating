using Datalayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace Datalayer.Repos {
    public class UserRepo : Repo<ApplicationUser, string>
    {
        private UserManager<ApplicationUser> manager;

        public UserRepo(ApplicationDbContext context) : base(context)
        {
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        public void AddUserToRole(string userId, string roleName) {
            manager.AddToRole(userId, roleName);
        }

        public string GetUserIdByEmail(string email) {
            return Items.Single(user => user.Email.Equals(email)).Id;
        }

    }
}
