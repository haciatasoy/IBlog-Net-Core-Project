using EntityLayer.Concreate;
using IBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IBlog.Controllers
{
    [Authorize(Roles = "Admin,Yazar")]
    public class TwoFactorController : Controller
    {
        private readonly AuthenticatorService authenticatorService;
        private readonly UserManager<AppUser> userManager;

        public TwoFactorController(AuthenticatorService authenticatorService, UserManager<AppUser> userManager)
        {
            this.authenticatorService = authenticatorService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user =await userManager.FindByNameAsync(User.Identity.Name);
            if (user.TwoFactorEnabled)
            {
                ViewData["TwoFactorMessage"] = "2-factor authentication is on, you can turn it off here if you wish.";
            }
            string sharedkey=await authenticatorService.GenerateSharedKey(user);
            string qrcodeUrl = await authenticatorService.GenerateQrCodeUri(sharedkey, "IBLOG", user);
            return View(new AuthenticatorVM
            {
                QrCodeUri = qrcodeUrl,
                SharedKey = sharedkey
            });
        }
        [HttpPost]
        public async Task<IActionResult> Index(AuthenticatorVM model)
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            VerifyState verifyState=await authenticatorService.Verify(model, user);
            if (verifyState.State)
            {
                TempData["message2"] = "İki adımlı doğrulama hesaba tanımlanmıştır.";
                TempData["message3"] = verifyState.RecoveryCode;
            }
            return View(model);
        }
    }
}
