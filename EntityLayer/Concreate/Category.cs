using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Category
    {
        [Key]
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public string? KategoriResim { get; set; }
        public bool Durum { get; set; }

        public List<Blog> Blog { get; set; }
    }
}
