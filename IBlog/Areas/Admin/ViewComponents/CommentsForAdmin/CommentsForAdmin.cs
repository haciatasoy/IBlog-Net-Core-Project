using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.Areas.Admin.ViewComponents.CommentsForAdmin
{
    public class CommentsForAdmin:ViewComponent
    {
        CommentManager cm=new CommentManager(new CommentRepository());
        public IViewComponentResult Invoke()
        {
            var comments = cm.GetWithBlogList();
            return View(comments);
        }
    }
}
