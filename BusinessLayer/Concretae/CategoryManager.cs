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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal CategoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            CategoryDal = categoryDal;
        }

        public void Delete(Category t)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetBlogCountAllCategory()
        {
            return CategoryDal.GetBlogCountAllCategory();
        }

        public List<Category> GetBlogCountWithCategory()
		{
			return CategoryDal.GetBlogCountWithCategory();
		}

		public Category GetById(int id)
        {
            return CategoryDal.GetById(id);
        }
		public List<Category> GetListAll()
        {
          return CategoryDal.GetAll();
        }

        public void Insert(Category t)
        {
           CategoryDal.Add(t);
        }

        public void Update(Category t)
        {
            CategoryDal.Update(t);
        }
    }
}
