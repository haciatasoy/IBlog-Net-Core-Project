using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Comment.CommentList
{
    public class CommentListByBlog : ViewComponent
    {
        CommentManager cm = new CommentManager(new CommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var comments = cm.GetCommentWithBlog(id);

            return View(comments);
        }
    }
}
