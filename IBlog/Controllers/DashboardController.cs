using BusinessLayer.Concretae;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFaremwork;
using IBlog.Areas.Admin.Models;
using IBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IBlog.Controllers
{
    [Authorize(Roles = "Admin,Yazar")]
    public class DashboardController : Controller
	{
		private readonly Context c;
		BlogManager bm = new BlogManager(new BlogRepository());
		CommentManager cm=new CommentManager(new CommentRepository());
        CategoryManager catm=new CategoryManager(new CategoryRepository());

        public DashboardController(Context c)
        {
            this.c = c;
        }

        public IActionResult Index()
		{
			var username = User.Identity.Name;
			var userid=c.Users.Where(x=>x.UserName==username).Select(x=>x.Id).FirstOrDefault();
			var usernamesurname=c.Users.Where(x=>x.UserName==username).Select(x=>x.NameSurname).FirstOrDefault();

			var comments=c.BlogComments.Where(x=>x.CommentUsername==usernamesurname).Count();
			ViewBag.Comments = comments;
			var bloglar = c.Blogs.Where(x => x.AppUserId == userid).Count();
			ViewBag.Blog = bloglar;
            //all messages for current user var messaages = c.Messages.Where(x => x.FromUserId == userid || x.ToUserMessageId==userid).Count();
            var messaages = c.Messages.Where(x => x.FromUserId == userid).Count();
            ViewBag.messages=messaages;
			return View();
		}
		public IActionResult Blogs()
		{
            var username = User.Identity.Name;
            var userid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            var values=bm.GetBlogsListWithWriter(userid);	
			return View(values);
		}
		public IActionResult Comments()
		{
			var username = User.Identity.Name;
            var userid = c.Users.Where(x => x.UserName == username).Select(x => x.NameSurname).FirstOrDefault();
			var values = cm.GetWithBlogList().Where(x=>x.CommentUsername==userid);
            return View(values);
		}
		public IActionResult CommentDelete(int id)
		{
			var value=cm.GetById(id);
			cm.Delete(value);
			return RedirectToAction("Comments");
		}
		public IActionResult BlogDelete(int id)
		{
            var blogcomments = cm.GetListAll().Where(x => x.BlogId == id).ToList();
            foreach (var blogcomment in blogcomments)
            {
                cm.Delete(blogcomment);
            }
            var value=bm.GetById(id);
            string imagedelete = value.BlogResim;
            string imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogPictures/", imagedelete);
            FileInfo fileInfo = new FileInfo(imagepath);
            if (fileInfo != null)
            {
                fileInfo.Delete();
            }
            bm.Delete(value);
			
			return RedirectToAction("Blogs");
		}
		[HttpGet]
		public IActionResult VisualizeBlogResult()
		{
            var username = User.Identity.Name;
            var userid = c.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            var values = catm.GetBlogCountAllCategory().Select(x => new CategoryChartModel
            {              
                categoryname = x.KategoriAdi,
                categorycount = x.Blog.Where(y=>y.AppUserId==userid).Count()
            }).ToList();

            return Json(new { jsonlist = values });
        }

    }
}
