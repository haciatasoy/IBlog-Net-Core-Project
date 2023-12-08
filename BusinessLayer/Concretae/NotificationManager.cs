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
    public class NotificationManager : INotificationService
    {
        INotificationDal NotificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            NotificationDal = notificationDal;
        }

        public void Delete(Notification t)
        {
            throw new NotImplementedException();
        }

        public Notification GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetListAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Notification t)
        {
            NotificationDal.Add(t);
        }

        public List<Notification> TGetAll3Last()
        {
            throw new NotImplementedException();
        }

        public void Update(Notification t)
        {
            throw new NotImplementedException();
        }
    }
}
