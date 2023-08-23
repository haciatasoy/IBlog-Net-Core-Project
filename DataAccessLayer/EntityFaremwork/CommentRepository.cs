using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using DataAccessLayer.Repository;
using EntityLayer.Concreate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFaremwork
{
    public class CommentRepository : GenericRepository<BlogComment>, ICommentDal
    {
        Context c = new Context();

        public List<BlogComment> GetListWithBlog(int id)
        {
            return c.BlogComments.Include(x=>x.Blog).Where(x=>x.BlogId==id).ToList();
        }

        public List<BlogComment> GetWithBlogList()
        {
            return c.BlogComments.Include(x=>x.Blog).OrderByDescending(x=>x.YorumTarih).ToList();
        }
    }
}
