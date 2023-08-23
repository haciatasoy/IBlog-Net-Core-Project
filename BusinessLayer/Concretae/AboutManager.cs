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
    public class AboutManager:IAboutService
    {
        IAboutDal AboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            AboutDal = aboutDal;
        }

        public void Delete(About t)
        {
            throw new NotImplementedException();
        }

        public About GetById(int id)
        {
            return AboutDal.GetById(id);
        }

        public List<About> GetListAll()
        {
           return AboutDal.GetAll();
        }

        public void Insert(About t)
        {
            throw new NotImplementedException();
        }

        public void Update(About t)
        {
            AboutDal.Update(t);
        }
    }
}
