using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Contact
{
    public class AddContactDto
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Mesaj { get; set; }
    }
}
