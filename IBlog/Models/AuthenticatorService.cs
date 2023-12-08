using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;

namespace IBlog.Models
{
    public class AuthenticatorService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly UrlEncoder urlEncoder;

        public AuthenticatorService(UserManager<AppUser> userManager, UrlEncoder urlEncoder)
        {
            this.userManager = userManager;
            this.urlEncoder = urlEncoder;
        }

        public async Task<string> GenerateSharedKey(AppUser user)
        {
            string sharedkey=await userManager.GetAuthenticatorKeyAsync(user);
            if(string.IsNullOrEmpty(sharedkey))
            {
                IdentityResult result=await userManager.ResetAuthenticatorKeyAsync(user);
                if (result.Succeeded)
                {
                    sharedkey = await userManager.GetAuthenticatorKeyAsync(user);
                }
                
            }
            return sharedkey;
        }
        public async Task<string> GenerateQrCodeUri(string sharedKey, string title, AppUser user) =>
        $"otpauth://totp/{urlEncoder.Encode(title)}:{urlEncoder.Encode(user.UserName)}?secret={sharedKey}&issuer={urlEncoder.Encode(title)}";

        public async Task<VerifyState> Verify(AuthenticatorVM model, AppUser user)
        {
            VerifyState verifyState = new VerifyState();
            verifyState.State = await userManager.VerifyTwoFactorTokenAsync(user, userManager.Options.Tokens.AuthenticatorTokenProvider, model.VerificationCode);
            if (verifyState.State)
            {
                user.TwoFactorEnabled = true;
                verifyState.RecoveryCode = await userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5);
            }
            return verifyState;
        }
    }

}
