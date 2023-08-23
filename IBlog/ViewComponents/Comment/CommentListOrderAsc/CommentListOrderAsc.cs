using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Comment.CommentListOrderAsc
{
    public class CommentListOrderAsc : ViewComponent
    {
        CommentManager cm = new CommentManager(new CommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var comments = cm.GetCommentWithBlog(id).Where(x => x.YorumPuan != null).OrderBy(x => x.YorumPuan);
            return View(comments);
        }
    }
}
