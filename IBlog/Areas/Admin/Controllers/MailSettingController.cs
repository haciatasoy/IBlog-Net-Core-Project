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
    public class MailSettingController : Controller
    {
        MailSettingManager mm=new MailSettingManager(new MailSettingRepository());
        public IActionResult Index()
        {
            var values = mm.GetListAll();
            MailSettingModel model = new MailSettingModel();
            model.MailSetting = values;
            return View(model);

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var value = mm.GetById(id);
            MailSettingModel model = new MailSettingModel();
            model.SmtpMail = value.SmtpMail;
            model.Usermail = value.Usermail;
            model.Password = value.Password;
            model.Port = value.Port;
            model.EnableCred = value.EnableCred;
            model.EnableSsl = value.EnableSsl;
            return RedirectToAction("Update",model);
        }
        [HttpPost]
        public IActionResult Update(MailSettingModel model)
        {
            var value=mm.GetById(model.Id);
            value.SmtpMail=model.SmtpMail;
            value.Port = model.Port;
            value.EnableCred = model.EnableCred;
            value.EnableSsl = model.EnableSsl;
            value.Usermail = model.Usermail;
            value.Password = model.Password;
            mm.Update(value);
            return RedirectToAction("Index");
        }
    }
}
