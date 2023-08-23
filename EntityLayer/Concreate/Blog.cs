using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public string BlogBaslik { get; set; }
        public string BlogIcerik { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public string BlogResim { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<BlogComment> Comment { get; set; }
    }
}
