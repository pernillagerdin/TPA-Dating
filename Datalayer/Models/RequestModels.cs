using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Models {
    public class RequestModels {
        [Key]
        public int RequestID { get; set; }

        [ForeignKey("RequestFrom")]
        public string RequestFromID { get; set; }
        public virtual ProfileModels RequestFrom {get; set;}

        [ForeignKey("RequestTo")]
        public string RequestToID { get; set; }
        public virtual ProfileModels RequestTo { get; set; }

        public DateTime RequestTimeStamp { get; set; }
    }
}
