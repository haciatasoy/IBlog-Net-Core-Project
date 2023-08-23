using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.Controllers
{
	[AllowAnonymous]
	public class AboutController : Controller
	{
		AboutManager am=new AboutManager(new AboutRepository());
		public IActionResult Index()
		{
			var values=am.GetListAll();
			return View(values);
		}
	}
}
