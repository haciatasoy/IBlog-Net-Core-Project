using BusinessLayer.Concretae;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFaremwork;
using DTOLayer.DTOs.Register;
using EntityLayer.Concreate;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace IBlog.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        MailSettingManager mm=new MailSettingManager(new MailSettingRepository());


		public RegisterController(UserManager<AppUser> userManager)
		{
			this.userManager = userManager;
			
		}

		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterCreateDto model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    NameSurname = model.NameSurname,
                    
                    Image = "https://i.hizliresim.com/645lujq.jpg"
                };
              
                if(!model.AcceptTerms)
                {
                    ViewData["ErrorMessage"] = "Lütfen şartları okuyup kabul ediniz!";
                    return View();
                }
                var result=await userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmlink = Url.Action("SuccessPage", "ConfirmMail", new { model.Email, token }, Request.Scheme);

                    var mailsetting = mm.GetListAll().FirstOrDefault();

                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("IBlog", mailsetting.Usermail);
                    MailboxAddress mailboxAddresTo = new MailboxAddress(user.NameSurname, user.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddresTo);

                    var bodybuilder = new BodyBuilder();
                    bodybuilder.TextBody = "Bu link üzerinden Email doğrulama işleminizi yapabilirsiniz:  " + confirmlink;
                    //"<a href=\""+confirmlink+"\" class=\"btn btn-primary\"> Doğrula</a>"

                    mimeMessage.Body = bodybuilder.ToMessageBody();

                    mimeMessage.Subject = "IBlog Mail Doğrulama";

                    SmtpClient client = new SmtpClient();
                    client.Connect(mailsetting.SmtpMail, mailsetting.Port, mailsetting.EnableSsl);
                    client.Authenticate(mailsetting.Usermail, mailsetting.Password);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                   

                    return RedirectToAction("Index", "ConfirmMail");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}
