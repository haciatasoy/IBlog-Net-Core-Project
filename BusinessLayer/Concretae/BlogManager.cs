using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concretae
{
    public class BlogManager : IBlogService
    {
        IBlogDal BlogDal;

        public BlogManager(IBlogDal blogDal)
        {
            BlogDal = blogDal;
        }

        public void Delete(Blog t)
        {
           BlogDal.Delete(t);
        }

        public List<Blog> GetBlogByID(int id)
        {
            return BlogDal.GetListById(id);
		}

        public List<Blog> GetBlogsListWithCategory()
        {
           return BlogDal.GetListCategory();
        }

        public List<Blog> GetBlogsListWithWriter(int id)
        {
            return BlogDal.GetListWithCategoryByWriter(id);
        }

        public Blog GetById(int id)
        {
           return BlogDal.GetById(id);
        }

		public List<Blog> GetCommentsByBlog()
		{
			return BlogDal.GetCommentsByBlog();
		}

		public List<Blog> GetLast3Blog()
        {
            return BlogDal.GetAll().TakeLast(3).ToList();
        }

        public List<Blog> GetListAll()
        {
           return BlogDal.GetAll();
        }

        public void Insert(Blog t)
        {
            throw new NotImplementedException();
        }

        public void Update(Blog t)
        {
            throw new NotImplementedException();
        }
    }
}
