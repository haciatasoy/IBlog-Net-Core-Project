using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Blog.CategoryBlogsDetailPage
{
    public class CategoryBlogs : ViewComponent
    {
        BlogManager bm = new BlogManager(new BlogRepository());
        public IViewComponentResult Invoke(int CategoryId, int blogid)
        {
            var exceptblog = bm.GetBlogByID(blogid);
            var values = bm.GetBlogsListWithCategory().Except(exceptblog).Where(x => x.CategoryId == CategoryId).Take(3);
            return View(values);
        }
    }
}
