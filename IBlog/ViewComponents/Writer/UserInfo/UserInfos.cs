using DataAccessLayer.Concreate;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Writer.UserInfo
{
    public class UserInfos : ViewComponent
    {
        private readonly Context c;

        public UserInfos(Context c)
        {
            this.c = c;
        }

        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var usernamesurname = c.Users.Where(x => x.UserName == username).Select(y => y.NameSurname).FirstOrDefault();
            var userphoto = c.Users.Where(x => x.UserName == username).Select(y => y.Image).FirstOrDefault();
            if (userphoto == "https://i.hizliresim.com/645lujq.jpg")
            {
                ViewBag.photo = userphoto;
            }
            else
            {
                ViewBag.photomain = userphoto;
            }
            ViewBag.name = usernamesurname;

            return View();
        }
    }
}
