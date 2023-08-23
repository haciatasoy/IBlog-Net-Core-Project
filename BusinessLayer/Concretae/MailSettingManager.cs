using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concretae
{
    public class MailSettingManager : ImailSettingService
    {
        IMailSettingDal mailSettingDal;

        public MailSettingManager(IMailSettingDal mailSettingDal)
        {
            this.mailSettingDal = mailSettingDal;
        }

        public void Delete(MailSetting t)
        {
            throw new NotImplementedException();
        }

        public MailSetting GetById(int id)
        {
            return mailSettingDal.GetById(id);
        }

        public List<MailSetting> GetListAll()
        {
            return mailSettingDal.GetAll();
        }

        public void Insert(MailSetting t)
        {
            throw new NotImplementedException();
        }

        public void Update(MailSetting t)
        {
            mailSettingDal.Update(t);
        }
    }
}
