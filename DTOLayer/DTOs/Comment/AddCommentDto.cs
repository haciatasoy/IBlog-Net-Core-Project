using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Comment
{
	public class AddCommentDto
	{
		public string? Yorum { get; set; }
		public double? YorumPuan { get; set; }
		public DateTime YorumTarih { get; set; }
		public string CommentUsername { get; set; }
		public string UserImage { get; set; }
		public int BlogId { get; set; }
		
	}
}
