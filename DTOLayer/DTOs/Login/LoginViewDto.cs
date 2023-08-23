using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Login
{
    public class LoginViewDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
