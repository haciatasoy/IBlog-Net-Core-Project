using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal: IGenericDal<Blog>
    {
        List<Blog> GetListById(int id);
        List<Blog> GetListCategory();
        List<Blog> GetListWithCategoryByWriter(int id);

        List<Blog> GetCommentsByBlog();
    }
}
