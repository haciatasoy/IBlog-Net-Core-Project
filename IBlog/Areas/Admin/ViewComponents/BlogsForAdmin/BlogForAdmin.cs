using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.Areas.Admin.ViewComponents.BlogsForAdmin
{
    public class BlogForAdmin:ViewComponent
    {
        BlogManager bm=new BlogManager(new BlogRepository());
        public IViewComponentResult Invoke()
        {
            var blogs = bm.GetBlogsListWithCategory();
            return View(blogs);
        }
    }
}
