using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Models {
    public class ContactModels {
        [Key]
        public int ContactID { get; set; }

        [ForeignKey("ContactA")]
        public string ContactAID { get; set; }
        public virtual ProfileModels ContactA {get; set;}

        [ForeignKey("ContactB")]
        public string ContactBID { get; set; }
        public virtual ProfileModels ContactB { get; set; }

    }
}
