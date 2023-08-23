using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using DataAccessLayer.Repository;
using EntityLayer.Concreate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFaremwork
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryDal
	{
		Context c=new Context();

        public List<Category> GetBlogCountAllCategory()
        {
            return c.Categories.Include(x=>x.Blog).ToList();
        }

        public List<Category> GetBlogCountWithCategory()
		{
			return c.Categories.Include(x => x.Blog).OrderByDescending(x=>x.Blog.Count()).Take(3).ToList();
		}
	}
}

