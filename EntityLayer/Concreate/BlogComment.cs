using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
	public class BlogComment
	{
        [Key]
        public int YorumId { get; set; }
        public string? Yorum { get; set; }
        public int? YorumPuan { get; set; }
        public DateTime YorumTarih { get; set; }
        public string CommentUsername { get; set; }
        public string UserImage { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
