using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Comment.CommentListOrderDesc
{
    public class CommentListOrderDesc : ViewComponent
    {
        CommentManager cm = new CommentManager(new CommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var comments = cm.GetCommentWithBlog(id).Where(x => x.YorumPuan != null).OrderByDescending(x => x.YorumPuan);
            return View(comments);
        }
    }
}
