using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using EntityLayer.Concreate;
using IBlog.Areas.Admin.Models;
using IBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace IBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        BlogManager bm=new BlogManager(new BlogRepository());
        private readonly UserManager<AppUser> userManager;
        CategoryManager cm=new CategoryManager(new CategoryRepository());
        CommentManager commn=new CommentManager(new CommentRepository());

        public HomeController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var blogs=bm.GetListAll().Count();
            var users=userManager.Users.Count();
            var kategoriler=cm.GetListAll().Count();
            var comments=commn.GetListAll().Count();
            ViewBag.blogs = blogs;
            ViewBag.users = users;
            ViewBag.kategoriler = kategoriler;
            ViewBag.comments = comments;
            return View();
        }
        [HttpGet]
        public IActionResult VisualizeBlogResult()
        {            
            var values = cm.GetBlogCountAllCategory().Select(x => new CategoryViewModel
            {
                categoryname = x.KategoriAdi,
                categorycount = x.Blog.Count()
            }).ToList();

            return Json(new { jsonlist = values });
        }
        [HttpGet]
        public IActionResult UserBlogList()
        {
            var values = userManager.Users.Select(x => new UserBlogModel
            {
                usernamesurname=x.NameSurname,
                usersblogcount=x.Blog.Count()
            }).ToList();
            return Json(new { jsonlist = values });
        }
       
    }
}
