using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Comment.AddComment
{
    public class AddComment : ViewComponent
    {
        BlogManager bm = new BlogManager(new BlogRepository());
        CommentManager cm = new CommentManager(new CommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var blogforcomment = bm.GetCommentsByBlog().Where(x => x.BlogId == id).FirstOrDefault();
            return View(blogforcomment);
        }
    }
}
