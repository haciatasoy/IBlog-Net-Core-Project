using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Register
{
    public class RegisterCreateDto
    {
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor!")]
        public string ConfirmPassword { get; set; }

        public bool AcceptTerms { get; set; }
    }
}
