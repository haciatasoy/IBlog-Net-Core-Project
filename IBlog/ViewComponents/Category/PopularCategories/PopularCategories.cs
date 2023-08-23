using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using IBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Category.PopularCategories
{
    public class PopularCategories : ViewComponent
    {
        CategoryManager cm = new CategoryManager(new CategoryRepository());
        public IViewComponentResult Invoke()
        {
            //var value = bm.GetBlogsListWithCategory().AsEnumerable().GroupBy(x => x.CategoryId).OrderByDescending(x => x.Count()).Take(3);
            //var value = cm.GetBlogCountWithCategory().GroupBy(x => x.KategoriAdi).Select(x => new GroupModel
            //{
            //	Category = x.Key.ToString(),
            //	BlogSayi = x.Count()
            //}).OrderByDescending(x => x.BlogSayi).Take(3);
            var values = cm.GetBlogCountWithCategory();
            var blogCountCategory = new List<GroupModel>();
            foreach (var value in values)
            {
                var categoryandBlogCount = new GroupModel();
                categoryandBlogCount.Category = value;
                categoryandBlogCount.BlogSayi = value.Blog.Count();
                blogCountCategory.Add(categoryandBlogCount);
            }

            return View(blogCountCategory);
        }
    }
}
