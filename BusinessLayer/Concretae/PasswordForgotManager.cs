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
    public class PasswordForgotManager : IPasswordForgotService
    {
        IPasswordForgotDal passwordForgotDal;

        public PasswordForgotManager(IPasswordForgotDal passwordForgotDal)
        {
            this.passwordForgotDal = passwordForgotDal;
        }

        public void Delete(PasswordForgotMail t)
        {
            passwordForgotDal.Delete(t);
        }

        public PasswordForgotMail getByCode(int code)
        {
            return passwordForgotDal.getByCode(code);
        }

        public PasswordForgotMail GetById(int id)
        {
           return passwordForgotDal.GetById(id);
        }

        public List<PasswordForgotMail> GetListAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(PasswordForgotMail t)
        {
            passwordForgotDal.Add(t);
        }

        public void Update(PasswordForgotMail t)
        {
            throw new NotImplementedException();
        }
    }
}
