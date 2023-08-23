using EntityLayer.Concreate;
using IBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace IBlog.Controllers
{
    [AllowAnonymous]
    public class ConfirmMailController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
			return View();
        }
        public async Task<IActionResult> SuccessPage(string email,string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await userManager.ConfirmEmailAsync(user, token);
            await userManager.AddToRoleAsync(user, "Yazar");
            return View(result.Succeeded ? "SuccessPage" : "Error");
        }
        public IActionResult Error()
        {
            return View();
        }
       
    }
}
