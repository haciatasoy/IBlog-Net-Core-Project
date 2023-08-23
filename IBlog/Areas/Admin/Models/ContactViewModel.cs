using EntityLayer.Concreate;

namespace IBlog.Areas.Admin.Models
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Mesaj { get; set; }

        public string MessageReturn { get; set; }

        public List<Contact> Contact { get; set; }
    }
}
