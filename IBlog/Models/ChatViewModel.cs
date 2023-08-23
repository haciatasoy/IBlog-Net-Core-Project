using EntityLayer.Concreate;

namespace IBlog.Models
{
    public class ChatViewModel
    {
        public string RecipientName { get; set; }
        public string NameSurname { get; set; }
        public string UserImage { get; set; }
        public List<Message> MyMessages { get; set; }
        public List<Message> OtherMessages { get; set; }
        public Message LastMessage { get; set; }
   
    }
}
