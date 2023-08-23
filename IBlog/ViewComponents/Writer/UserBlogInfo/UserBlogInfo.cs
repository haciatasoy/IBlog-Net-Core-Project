using DataAccessLayer.Concreate;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IBlog.ViewComponents.Writer.UserBlogInfo
{
    public class UserBlogInfo : ViewComponent
    {

        private readonly UserManager<AppUser> userManager;

        public UserBlogInfo(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = User.Identity.Name;
            var usernamesurname = await userManager.Users.Where(x => x.UserName == user).Select(y => y.NameSurname).FirstOrDefaultAsync();     //c.Users.Where(x=>x.UserName==user).Select(y=>y.NameSurname).FirstOrDefault();
            if (usernamesurname != null)
            {
                ViewBag.usernamesurname = usernamesurname;
            }
            else
            {
                ViewBag.info = "Welcome Back";
            }
            return View();
        }
    }
}
