using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using IBlog.Areas.Admin.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Data;

namespace IBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        ContactManager cm=new ContactManager(new ContactRepository());
        MailSettingManager mm = new MailSettingManager(new MailSettingRepository());
        public IActionResult Index()
        {
            var values = cm.GetListAll();
            ContactViewModel model = new ContactViewModel();
            model.Contact = values;
            return View(model);
        }
        [HttpGet]
        public IActionResult SendMail(int id)
        {
            var value=cm.GetById(id);
            ContactViewModel model = new ContactViewModel();
            model.Email = value.Email;
            model.NameSurname = value.NameSurname;
            model.Mesaj = value.Mesaj;
            return RedirectToAction("SendMail", model);
        }
        [HttpPost]
        public IActionResult SendMail(ContactViewModel model)
        {
            var value = cm.GetById(model.ContactId);
            var mailsetting = mm.GetListAll().FirstOrDefault();
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("IBlog", mailsetting.Usermail);
            MailboxAddress mailboxAddresTo = new MailboxAddress(value.NameSurname, value.Email);

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddresTo);

            var bodybuilder = new BodyBuilder();
            bodybuilder.TextBody = model.MessageReturn;
            mimeMessage.Body=bodybuilder.ToMessageBody();
           

            mimeMessage.Body = bodybuilder.ToMessageBody();

            mimeMessage.Subject = "IBlog Human Resources";

            SmtpClient client = new SmtpClient();
            client.Connect(mailsetting.SmtpMail, mailsetting.Port, mailsetting.EnableSsl);
            client.Authenticate(mailsetting.Usermail, mailsetting.Password);
            client.Send(mimeMessage);
            client.Disconnect(true);
            value.IsMailSend = true;
            cm.Update(value);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var deletecontact=cm.GetById(id);
            if(deletecontact != null)
            {
                cm.Delete(deletecontact);
                return RedirectToAction("Index");
            }
            return View();
        }
        

        
    }
}
