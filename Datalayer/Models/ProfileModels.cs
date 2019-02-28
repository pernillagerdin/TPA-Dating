using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Models
{
    public class ProfileModels
    {
        [Key]
        [ForeignKey("User")]
        public string ProfileID { get; set; }
        public virtual ApplicationUser User { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please fill in your first name")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Please enter a first name with between 1 - 25 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please fill in your last name")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Please enter a last name with between 1 - 25 characters")]
        public string LastName { get; set; }

        [Display(Name = "Biograpthy")]
        [Required(ErrorMessage = "Please give us some fun info about yourself")]
        public string Bio { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Please enter a valid birthdate")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter your gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }
       
    }

    public enum Gender { Woman, Man, Other, Unicorn}
}   
