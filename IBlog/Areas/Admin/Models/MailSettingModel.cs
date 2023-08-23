using EntityLayer.Concreate;

namespace IBlog.Areas.Admin.Models
{
    public class MailSettingModel
    {
        public int Id { get; set; }
        public string SmtpMail { get; set; }
        public string Usermail { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool EnableCred { get; set; }

        public List<MailSetting> MailSetting { get; set; }
    }
}
