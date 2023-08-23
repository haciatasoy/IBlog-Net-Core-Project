using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Blog.WritersBlogsDetailPage
{
    public class WritersBlogs : ViewComponent
    {
        BlogManager bm = new BlogManager(new BlogRepository());
        public IViewComponentResult Invoke(int id, int blogid)
        {
            var exceptblog = bm.GetBlogByID(blogid);
            var writerblogs = bm.GetBlogsListWithWriter(id).Except(exceptblog).OrderBy(x => Guid.NewGuid()).Take(3);
            return View(writerblogs);
        }
    }
}
