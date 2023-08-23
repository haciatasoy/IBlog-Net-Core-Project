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
    public class ContactManager : IContactService
    {
        IContactDal ContactDal;

        public ContactManager(IContactDal contactDal)
        {
            ContactDal = contactDal;
        }

        public void Delete(Contact t)
        {
            ContactDal.Delete(t);
        }

        public Contact GetById(int id)
        {
            return ContactDal.GetById(id);
        }

        public List<Contact> GetListAll()
        {
            return ContactDal.GetAll();
        }

        public void Insert(Contact t)
        {
            ContactDal.Add(t);
        }

        public void Update(Contact t)
        {
           ContactDal.Update(t);
        }
    }
}
