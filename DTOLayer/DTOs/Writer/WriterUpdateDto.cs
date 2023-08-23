using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Writer
{
	public class WriterUpdateDto
	{
		public string NameSurname { get; set; }
		public string UserName { get; set; }
        public IFormFile Image { get; set; }
        public string Email { get; set; }
        [MinLength(8)]
        [RegularExpression("(?=[A-Za-z0-9@#$%^&+!=<>',.]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=<>',.])(?=.{8,}).*$", ErrorMessage = "Password must contain at least one upper letter,lower letter,number and punctuation!")]
        public string Password { get; set; }
		[Compare("Password", ErrorMessage = "Parolalar uyuşmuyor!")]
		public string ConfirmPassword { get; set; }
	}
}
