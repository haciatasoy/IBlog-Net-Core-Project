namespace IBlog.Models
{
    public class TwoFactorLogin
    {
        public string VerifyCode { get; set; }
        public bool Recovery { get; set; }
    }
}
