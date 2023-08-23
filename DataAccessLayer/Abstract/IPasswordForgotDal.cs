using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IPasswordForgotDal:IGenericDal<PasswordForgotMail>
    {
        PasswordForgotMail getByCode(int code);
    }
}
