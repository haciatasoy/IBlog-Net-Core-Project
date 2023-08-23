using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using EntityLayer.Concreate;
using IBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace IBlog.Controllers
{
	[AllowAnonymous]
	public class BlogController : Controller
	{
		BlogManager bm = new BlogManager(new BlogRepository());
		CommentManager cm = new CommentManager(new CommentRepository());
		public IActionResult Index(string search,int page = 1)
		{
			if (!string.IsNullOrEmpty(search))
			{
				var blogwithsearch = bm.GetBlogsListWithCategory().Where(x => x.BlogBaslik.Contains(search) ||x.Category.KategoriAdi.ToString()==search || x.AppUser.NameSurname.ToString()==search).ToPagedList(page,3);
				ViewBag.Search = search;
				return View(blogwithsearch);
			}
			var bloglar =  bm.GetBlogsListWithCategory().ToPagedList(page, 3);
			return View(bloglar);
		}
		public IActionResult BlogDetail(int id)
		{
			ViewBag.i = id;
			TempData["id"]=id;
			var commentcount = cm.GetCommentWithBlog(id).Count();
			var blogpuan = cm.GetCommentWithBlog(id).Average(x => x.YorumPuan);
			ViewBag.commentcount = commentcount;
			ViewBag.blogpuan = blogpuan;
			var blog = bm.GetBlogByID(id);
			var writerblog = bm.GetBlogByID(id).Select(x => x.AppUserId).FirstOrDefault();
			var categoryblogs = bm.GetBlogByID(id).Select(x => x.CategoryId).FirstOrDefault();
			ViewBag.WriterId = writerblog;
			ViewBag.CategoryId = categoryblogs;
			return View(blog);
		}
		public IActionResult Category(string search,int id, int page = 1)
		{
            if (!string.IsNullOrEmpty(search))
            {
                var blogwithsearch = bm.GetBlogsListWithCategory().Where(x => x.BlogBaslik.Contains(search) || x.Category.KategoriAdi.ToString() == search || x.AppUser.NameSurname.ToString() == search).ToPagedList(page, 3);
                ViewBag.Search = search;
                return View(blogwithsearch);
            }
            var blogbycat = bm.GetBlogsListWithCategory().Where(x => x.CategoryId == id).ToPagedList(page, 3);
			ViewBag.id = id;
			return View(blogbycat);
		}
		public IActionResult Writer(string search,int id, int page = 1)
		{
            if (!string.IsNullOrEmpty(search))
            {
                var blogwithsearch = bm.GetBlogsListWithCategory().Where(x => x.BlogBaslik.Contains(search) || x.Category.KategoriAdi.ToString() == search || x.AppUser.NameSurname.ToString() == search).ToPagedList(page, 3);
				
                ViewBag.Search = search;
                return View(blogwithsearch);
            }
            var blogbywriter = bm.GetBlogsListWithWriter(id).ToPagedList(page, 3);
			
			ViewBag.id = id;
			return View(blogbywriter);
		}
	}
}
