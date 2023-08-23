using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Writer.MainPageWriters
{
    public class MainPageWriters : ViewComponent
    {
        private readonly UserManager<AppUser> userManager;

        public MainPageWriters(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            var writers = userManager.Users.Where(x => x.EmailConfirmed == true).Take(10).ToList();
            return View(writers);
        }
    }
}
