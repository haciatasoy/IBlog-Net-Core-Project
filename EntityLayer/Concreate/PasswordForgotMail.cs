using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class PasswordForgotMail
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
