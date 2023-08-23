using DataAccessLayer.Concreate;
using EntityLayer.Concreate;
using IBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IBlog.ViewComponents.Message.DashboardMessage
{
    public class DashboardMessage : ViewComponent
    {
        Context c = new Context();
        private readonly UserManager<AppUser> userManager;

        public DashboardMessage(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = User.Identity.Name;
            var userid = userManager.Users.Where(x => x.UserName == user).Select(x => x.Id).FirstOrDefault();
            var messagecount = c.Messages.Where(x => x.ToUserMessageId == userid).Count();
            ViewBag.MessageCount = messagecount;
            var messages = c.Messages.Include(x => x.FromUser).Where(x => x.ToUserMessageId == userid).OrderByDescending(x => x.MesajTarih).ToList().Take(3);
            return View(messages);
        }
    }
}
