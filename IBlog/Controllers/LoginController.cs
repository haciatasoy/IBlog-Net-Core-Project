using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using DTOLayer.DTOs.Login;
using EntityLayer.Concreate;
using IBlog.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace IBlog.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> signInManager;
		private readonly UserManager<AppUser> userManager;
		PasswordForgotManager pfm=new PasswordForgotManager(new PasswordForgotRepository());
		MailSettingManager ms=new MailSettingManager(new MailSettingRepository());

		public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index(string ReturnUrl = "")
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(string ReturnUrl, LoginViewDto model)
		{
			AppUser user = await userManager.FindByNameAsync(model.Username);
			Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,true);
			if (user.EmailConfirmed == false)
			{
				ViewData["ErrorMessage"] = "Invalid User";
				return View();
			}
			else
			{
				if (result.Succeeded)
				{

					
						if (string.IsNullOrEmpty(ReturnUrl))
							return RedirectToAction("Index", "Dashboard");
						return Redirect(ReturnUrl);	

				}
				else if (result.RequiresTwoFactor)
					return RedirectToAction("TwoFactor", new { ReturnUrl = ReturnUrl });
				else
				{
					ViewData["ErrorMessage"] = "Kullanıcı adı veya parola yanlış!";
					return View();
				}
			}
			
			
		}
		[HttpGet]
		public async Task <IActionResult> TwoFactor()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> TwoFactor(TwoFactorLogin model)
        {
			AppUser user = await signInManager.GetTwoFactorAuthenticationUserAsync();
			Microsoft.AspNetCore.Identity.SignInResult result = null;
			if (model.Recovery)
			{
				result = await signInManager.TwoFactorRecoveryCodeSignInAsync(model.VerifyCode);
			}
			else
			{
				result = await signInManager.TwoFactorAuthenticatorSignInAsync(model.VerifyCode, true, false);
			}
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Dashboard");
			}
			else
			{
				ViewData["ErrorMessage"] = "Doğrulama kodu yanlış!";
				return View();
			}
            return View();
        }
        public async Task<IActionResult> SignOut()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index");
		}
		public IActionResult AccessDenied()
		{
			return View();
		}
		public IActionResult ForgotPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendCode(string email)
		{
            Random rnd = new Random();
            int code;
            code = rnd.Next(100000, 1000000);
            var mailsetting = ms.GetListAll().FirstOrDefault();
			var user=userManager.Users.Where(x=>x.Email == email).FirstOrDefault();
			if (user != null)
			{
				pfm.Insert(new PasswordForgotMail {AppUserId=user.Id,Code=code });
				MimeMessage message = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("IBLOG", mailsetting.Usermail);
                MailboxAddress mailboxAddresTo = new MailboxAddress(user.NameSurname, user.Email);

                message.From.Add(mailboxAddressFrom);
                message.To.Add(mailboxAddresTo);

                var bodybuilder = new BodyBuilder();
                bodybuilder.TextBody = "Parolanızı Yeniden oluşturmak için tek kullanımlık kodunuz: " + code;
                message.Body = bodybuilder.ToMessageBody();

                message.Subject = "Yeniden Parola Oluşturma Kodu";

                SmtpClient client = new SmtpClient();
                client.Connect(mailsetting.SmtpMail, mailsetting.Port, false);
                client.Authenticate(mailsetting.Usermail, mailsetting.Password);
                client.Send(message);
                client.Disconnect(true);
				return RedirectToAction("ResetPassword");
            }
			else
			{
                return NotFound();
            }
			return View();
		}
		[HttpGet]
		public IActionResult ResetPassword()
		{
			return View();
		}
        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if(ModelState.IsValid)
			{
				var passcode = pfm.getByCode(model.code);
				if (passcode != null)
				{
					var user = await userManager.FindByIdAsync(passcode.AppUserId.ToString());
					if(model.password!= null)
					{
						user.PasswordHash=userManager.PasswordHasher.HashPassword(user,model.password);
					}
					var result=await userManager.UpdateAsync(user);
					if(result.Succeeded)
					{
						pfm.Delete(passcode);
						return RedirectToAction("Index");
					}
                    else
                    {
                        foreach (var x in result.Errors)
                        {
                            ModelState.AddModelError("", x.Description);
                        }
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "Böyle bir kod yok";
                    return View();
                }
            }
			return View(model);
		}

    }
}
