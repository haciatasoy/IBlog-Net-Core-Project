namespace IBlog.Models
{
    public class AuthenticatorVM
    {
        public string SharedKey { get; set; }
        public string QrCodeUri { get; set; }
        public string VerificationCode { get; set; }
    }
}
