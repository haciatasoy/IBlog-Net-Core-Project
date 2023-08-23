using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    [Index(nameof(UserName), IsUnique = true)]
    public class AppUser:IdentityUser<int>
    {
        public string NameSurname { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Message> Sender { get; set; }
        public virtual ICollection<Message> Receiver { get; set; }

        public List<Blog> Blog { get; set; }

    }
}
