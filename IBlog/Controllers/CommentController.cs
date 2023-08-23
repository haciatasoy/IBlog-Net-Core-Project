using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using DTOLayer.DTOs.Comment;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IBlog.Controllers
{
	public class CommentController : Controller
	{
		CommentManager cm=new CommentManager(new CommentRepository());
		private readonly UserManager<AppUser> userManager;

		public CommentController(UserManager<AppUser> userManager)
		{
			this.userManager = userManager;
		}

		
		[Authorize(Roles = "Admin,Yazar")]
		[HttpPost]
		public async Task<IActionResult> AddComment(BlogComment comment)
		{
			var user = User.Identity.Name;
			var usernamesurname=userManager.Users.Where(x=>x.UserName==user).Select(x=>x.NameSurname).FirstOrDefault();
			var userimage=userManager.Users.Where(x=>x.UserName==user).Select(x=>x.Image).FirstOrDefault();
			comment.YorumTarih= DateTime.Parse(DateTime.Now.ToLongDateString());
			comment.CommentUsername = usernamesurname;
			comment.UserImage = userimage;
			var blogid = TempData["id"];

            cm.Insert(comment);
			return RedirectToAction("BlogDetail", "Blog", new {id=blogid});
		}
	}
}
