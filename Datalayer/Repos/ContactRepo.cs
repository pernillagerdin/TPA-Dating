using Datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Repos {
    public class ContactRepo : Repo<ContactModels, int> {
        public ContactRepo(ApplicationDbContext context) : base(context) { }

        public List<ContactModels> GetContacts(string profileID) {
            return Items.Where(contact => contact.ContactAID.Equals(profileID) || contact.ContactBID.Equals(profileID)).ToList();
        }
        public bool Contacts (string currentProfileID, string profileID) {
            return Items.Any(contact => 
            contact.ContactAID.Equals(currentProfileID) && 
            contact.ContactBID.Equals(profileID) ||
            contact.ContactBID.Equals(currentProfileID) &&
            contact.ContactAID.Equals(profileID)
            );
        }
    }
}
