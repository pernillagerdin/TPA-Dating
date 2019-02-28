using Datalayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Datalayer.Repos
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ProfileModels> Profiles { get; set; }
        public DbSet<PostModels> Posts { get; set; }
        public DbSet<ContactModels> Contacts { get; set; }
        public DbSet<RequestModels> Requests { get; set; }
    }
}
