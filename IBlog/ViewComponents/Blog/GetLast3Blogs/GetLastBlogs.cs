using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Blog.GetLast3Blogs
{

    public class GetLastBlogs : ViewComponent
    {
        BlogManager bm = new BlogManager(new BlogRepository());
        public IViewComponentResult Invoke()
        {
            var blogs = bm.GetLast3Blog();
            return View(blogs);
        }
    }
}
