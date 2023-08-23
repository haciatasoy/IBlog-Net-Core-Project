using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Message
    {
        [Key]
        public int MessageıD { get; set; }
        public string Mesaj { get; set; }
        public DateTime MesajTarih { get; set; }       
        public int ToUserMessageId { get; set; }
        public virtual AppUser ToUserMessage { get; set; }
        public int FromUserId { get; set; }
        public virtual AppUser FromUser { get; set; }
    }
}
