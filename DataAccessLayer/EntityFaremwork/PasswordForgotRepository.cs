using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using DataAccessLayer.Repository;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFaremwork
{
    public class PasswordForgotRepository : GenericRepository<PasswordForgotMail>, IPasswordForgotDal
    {
        Context c=new Context();
        public PasswordForgotMail getByCode(int code)
        {
            return c.PasswordForgotMails.Where(x => x.Code == code).FirstOrDefault();
        }
    }
}
