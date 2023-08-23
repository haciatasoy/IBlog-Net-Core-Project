using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        CommentManager cm=new CommentManager(new CommentRepository());
        public IActionResult Index()
        {
            var values=cm.GetWithBlogList();
            return View(values);
        }
        public IActionResult Delete(int id)
        {
            var value=cm.GetById(id);
            cm.Delete(value);
            return RedirectToAction("Index");
        }
    }
}
