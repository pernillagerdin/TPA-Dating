using Datalayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPA_DatingMVC.Models
{
    public class PostViewModel {
        public List<PostModels> Posts { get; set; }
    }

    public class ContactViewModel {
        public string CurrentProfileID { get; set; }
        public List<ContactModels> Contacts { get; set; }
    }

    public class ProfileViewModel
    {   
        public string CurrentUser { get; set; }
        public string ProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public byte[] ProfilePicture { get; set; }

        public PostViewModel Posts { get; set; }

        public string Relation { get; set; }

        public ContactViewModel Contacts {get; set;}
    }
}