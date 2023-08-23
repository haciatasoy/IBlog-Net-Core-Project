using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Mesaj { get; set; }

        public bool IsMailSend { get; set; }
    }
}
