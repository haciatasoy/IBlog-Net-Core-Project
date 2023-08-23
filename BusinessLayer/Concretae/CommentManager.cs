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
    public class CommentManager : ICommentService
    {
        ICommentDal CommentDal;

        public CommentManager(ICommentDal commentDal)
        {
            CommentDal = commentDal;
        }

        public void Delete(BlogComment t)
        {
            CommentDal.Delete(t);
        }

        public BlogComment GetById(int id)
        {
            return CommentDal.GetById(id);
        }

        public List<BlogComment> GetCommentWithBlog(int id)
        {
            return CommentDal.GetListWithBlog(id);
        }

        public List<BlogComment> GetListAll()
        {
            return CommentDal.GetAll();
        }

        public List<BlogComment> GetWithBlogList()
        {
            return CommentDal.GetWithBlogList();
        }

        public void Insert(BlogComment t)
        {
            CommentDal.Add(t);
        }

        public void Update(BlogComment t)
        {
            throw new NotImplementedException();
        }
    }
}
