using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Models {
    public class PostModels {
        [Key]
        public int PostID { get; set; }
        
        [ForeignKey("PostFrom")]
        public string PostFromID { get; set; }
        public virtual ProfileModels PostFrom { get; set; }

        [Required]
        [ForeignKey("PostTo")]
        public string PostToID { get; set; }
        public virtual ProfileModels PostTo { get; set; }


        [Required]
        public string PostContent { get; set; }

        public DateTime PostTimeStamp { get; set; }
    }
}
