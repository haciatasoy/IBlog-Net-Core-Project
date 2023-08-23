using DataAccessLayer.Concreate;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Writer.UserInfoMain
{
    public class UserInfoMains : ViewComponent
    {
        private readonly Context c;

        public UserInfoMains(Context c)
        {
            this.c = c;
        }
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var usernamesurname = c.Users.Where(x => x.UserName == username).Select(y => y.NameSurname).FirstOrDefault();
            var userphoto = c.Users.Where(x => x.UserName == username).Select(y => y.Image).FirstOrDefault();
            ViewBag.name = usernamesurname;

            if (userphoto == "https://i.hizliresim.com/645lujq.jpg")
            {
                ViewBag.photo = userphoto;
            }
            else
            {
                ViewBag.photomain = userphoto;
            }

            return View();
        }
    }
}
