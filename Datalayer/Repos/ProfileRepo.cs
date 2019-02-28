using Datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Repos
{
    public class ProfileRepo : Repo<ProfileModels, string>
    {
        public ProfileRepo(ApplicationDbContext context) : base(context) { }
        public List<ProfileModels> GetAllExceptCurrent(string profileId)
        {
            return Items.Where(profile => !profile.ProfileID.Equals(profileId)).ToList();
        }
        public bool IfProfileExists(string profileId)
        {
            return Items.Any(profile => profile.ProfileID.Equals(profileId));
        }
    }
}
