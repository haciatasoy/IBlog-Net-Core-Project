using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        BlogManager bm=new BlogManager(new BlogRepository());
        CommentManager cm=new CommentManager(new CommentRepository());
        public IActionResult Index()
        {
            var values = bm.GetBlogsListWithCategory();
            return View(values);
        }
        public IActionResult Delete(int id)
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
            return RedirectToAction("Index");
        }
    }
}
