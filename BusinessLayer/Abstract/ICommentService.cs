using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<BlogComment>
    {
        List<BlogComment> GetCommentWithBlog(int id);
        List<BlogComment> GetWithBlogList();
    }
}
