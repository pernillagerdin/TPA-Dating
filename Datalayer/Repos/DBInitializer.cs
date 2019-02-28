using Datalayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.IO;

namespace Datalayer.Repos
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
            SeedUsers(context);
        }

        public static byte[] SetInitialProfilePicture(string endPath)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + endPath;
            using (var file = new FileStream(path, FileMode.Open)) {
                using (var binary = new BinaryReader(file)) {
                    return binary.ReadBytes((int)file.Length);
                }
            }
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            IdentityRole hasProfile = new IdentityRole { Name = "hasProfile" };
            roleManager.Create(hasProfile);


            ApplicationUser pernillaU = new ApplicationUser {
                UserName = "pernilla@tpa.se",
                Email = "pernilla@tpa.se",
                DisplayName = "Pernilla"
            };
            userManager.Create(pernillaU, "password");
            userManager.AddToRole(pernillaU.Id, "hasProfile");

            ApplicationUser tobiasU = new ApplicationUser {
                UserName = "tobias@tpa.se",
                Email = "tobias@tpa.se",
                DisplayName = "Tobias"
            };
            userManager.Create(tobiasU, "password");
            userManager.AddToRole(tobiasU.Id, "hasProfile");

            ApplicationUser andreasU = new ApplicationUser {
                UserName = "andreas@tpa.se",
                Email = "andreas@tpa.se",
                DisplayName = "Andreas"
            };
            userManager.Create(andreasU, "password");
            userManager.AddToRole(andreasU.Id, "hasProfile");

            ApplicationUser testU = new ApplicationUser {
                UserName = "test@tpa.se",
                Email = "test@tpa.se",
                DisplayName = "Test"
            };
            userManager.Create(testU, "password");
            userManager.AddToRole(testU.Id, "hasProfile");

            //Create profiles
            ProfileModels pernillaP = new ProfileModels {
                ProfileID = pernillaU.Id,
                FirstName = "Pernilla",
                LastName = "Gerdin",
                Bio = "I added a unicorn as a gender hihi",
                DateOfBirth = new DateTime(1997, 11, 16),
                Gender = Gender.Woman,
                ProfilePicture = SetInitialProfilePicture("/content/images/defaultProfile.png")

            };
            ProfileModels tobiasP = new ProfileModels {
                ProfileID = tobiasU.Id,
                FirstName = "Tobias",
                LastName = "Kostet",
                Bio = "I added a unicorn as a gender hihi",
                DateOfBirth = new DateTime(1997, 11, 16),
                Gender = Gender.Man,
                ProfilePicture = SetInitialProfilePicture("/content/images/defaultProfile.png")
            };
            ProfileModels andreasP = new ProfileModels {
                ProfileID = andreasU.Id,
                FirstName = "Andreas",
                LastName = "Johansson",
                Bio = "I added a unicorn as a gender hihi",
                DateOfBirth = new DateTime(1997, 11, 16),
                Gender = Gender.Man,
                ProfilePicture = SetInitialProfilePicture("/content/images/defaultProfile.png")

            };

            ProfileModels testP = new ProfileModels {
                ProfileID = testU.Id,
                FirstName = "Test",
                LastName = "User",
                Bio = "I am here to break things mohahah",
                DateOfBirth = new DateTime(1997, 11, 16),
                Gender = Gender.Unicorn,
                ProfilePicture = SetInitialProfilePicture("/content/images/defaultProfile.png")

            };
            //Create posts

            PostModels post1 = new PostModels {
                PostFromID = pernillaU.Id,
                PostToID = tobiasU.Id,
                PostContent = "This is a post",
                PostTimeStamp = new DateTime(2019, 02, 27, 21, 36, 00)
            };

            PostModels post2 = new PostModels {
                PostFromID = tobiasU.Id,
                PostToID = andreasU.Id,
                PostContent = "This is a post",
                PostTimeStamp = new DateTime(2019, 02, 27, 21, 37, 00)
            };

            PostModels post3 = new PostModels {
                PostFromID = andreasU.Id,
                PostToID = pernillaU.Id,
                PostContent = "This is a post",
                PostTimeStamp = new DateTime(2019, 02, 27, 21, 38, 00)
            };

            PostModels post4 = new PostModels {
                PostFromID = andreasU.Id,
                PostToID = tobiasU.Id,
                PostContent = "This is a post",
                PostTimeStamp = new DateTime(2019, 02, 26, 21, 26, 00)
            };

            PostModels post5 = new PostModels {
                PostFromID = tobiasU.Id,
                PostToID = pernillaU.Id,
                PostContent = "This is a post",
                PostTimeStamp = new DateTime(2018, 12, 27, 11, 36, 00)
            };

            PostModels post6 = new PostModels {
                PostFromID = pernillaU.Id,
                PostToID = andreasU.Id,
                PostContent = "This is a post",
                PostTimeStamp = new DateTime(2017, 09, 27, 12, 16, 00)
            };

            context.Profiles.AddRange(new[] { pernillaP, tobiasP, andreasP, testP }); //Begin transactionpost
            context.Posts.AddRange(new[] { post1, post2, post3, post4, post5, post6 });
            context.SaveChanges(); //Commit transaction

            
        }

    }
}
