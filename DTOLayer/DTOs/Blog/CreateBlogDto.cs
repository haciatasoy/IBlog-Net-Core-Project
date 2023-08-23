using EntityLayer.Concreate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Blog
{
    public class CreateBlogDto
    {
        public string BlogBaslik { get; set; }
        public string BlogIcerik { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public IFormFile BlogResim { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
