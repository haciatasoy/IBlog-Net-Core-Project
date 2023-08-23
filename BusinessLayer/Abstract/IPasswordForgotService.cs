using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPasswordForgotService:IGenericService<PasswordForgotMail>
    {
        PasswordForgotMail getByCode(int code);
    }
}
