using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class About
    {
        [Key]
        public int AboutId { get; set; }
        public string Mail { get; set; }
        public string Adres { get; set; }
        public string MapLocation { get; set; }
        public string Telefon { get; set; }
    }
}
