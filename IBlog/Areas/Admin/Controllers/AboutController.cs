using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using IBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        AboutManager am=new AboutManager(new AboutRepository());
        public IActionResult Index()
        {
            var values=am.GetListAll();
            AboutViewModel model=new AboutViewModel();
            model.About = values;
            return View(model);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var value=am.GetById(id);
            AboutViewModel model = new AboutViewModel();
            model.MapLocation = value.MapLocation;
            model.Telefon = value.Telefon;
            model.Adres = value.Adres;
            model.Mail = value.Mail;
            return RedirectToAction("Update", model);
        }
        [HttpPost]
        public IActionResult Update(AboutViewModel model)
        {
            var value = am.GetById(model.AboutId);
            value.MapLocation = model.MapLocation;
            value.Adres = model.Adres;
            value.Telefon=model.Telefon;
            value.Mail = model.Mail;
            am.Update(value);
            return RedirectToAction("Index");
        }
    }
}
