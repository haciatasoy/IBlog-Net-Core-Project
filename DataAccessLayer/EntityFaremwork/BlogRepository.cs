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
    public class BlogRepository : GenericRepository<Blog>, IBlogDal
    {
       Context c=new Context();

		public List<Blog> GetCommentsByBlog()
		{
            return c.Blogs.Include(x => x.Comment).ToList();
		}

		public List<Blog> GetListById(int id)
		{
			return c.Blogs.Include(x => x.Category).Include(x => x.AppUser).Where(x=>x.BlogId == id).ToList();
		}

		public List<Blog> GetListCategory()
        {
            return c.Blogs.Include(x => x.Category).Include(x=>x.AppUser).Include(x=>x.Comment).OrderByDescending(y=>y.BlogId).ToList();
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            return c.Blogs.Include(x => x.Category).Include(x=>x.AppUser).Include(x=>x.Comment).Where(x => x.AppUserId == id).OrderByDescending(x=>x.BlogId).ToList();
        }
    }
}
