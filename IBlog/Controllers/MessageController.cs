using DataAccessLayer.Concreate;
using EntityLayer.Concreate;
using IBlog.Hubs;
using IBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace IBlog.Controllers
{
    [Authorize(Roles = "Admin,Yazar")]
    public class MessageController : Controller
    {
        Context c = new Context();
        private readonly UserManager<AppUser> userManager;
        private readonly IHubContext<ChatHub> hub;

        public MessageController(UserManager<AppUser> userManager, IHubContext<ChatHub> hub)
        {
            this.userManager = userManager;
            this.hub = hub;
        }

        public async Task<IActionResult> Index(string search)
        {
            if (!string.IsNullOrEmpty(search)) {
                var user = await userManager.GetUserAsync(HttpContext.User);


                var messages = await c.Messages.Where(x => x.ToUserMessageId == user.Id || x.FromUserId == user.Id).Include(x => x.FromUser).OrderByDescending(x => x.MesajTarih).ToListAsync();

                var chats = new List<ChatViewModel>();
                foreach (var i in await c.Users?.Where(x=>x.NameSurname.Contains(search) && x.Id != user.Id).ToListAsync())
                {
                    if (i == user) continue;

                    var chat = new ChatViewModel()
                    {
                        MyMessages = messages.Where(x => x.FromUserId == user.Id && x.ToUserMessageId == i.Id).ToList(),
                        OtherMessages = messages.Where(x => x.FromUserId == i.Id && x.ToUserMessageId == user.Id).ToList(),
                        RecipientName = i.UserName,
                        NameSurname = i.NameSurname,
                        UserImage = i.Image

                    };

                    var chatMessages = new List<Message>();
                    chatMessages.AddRange(chat.MyMessages);
                    chatMessages.AddRange(chat.OtherMessages);

                    chat.LastMessage = chatMessages.OrderByDescending(x => x.MesajTarih).FirstOrDefault();

                    chats.Add(chat);


                }
                return View(chats);
            }
            else
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                

                var messages = await c.Messages.Where(x => x.ToUserMessageId == user.Id || x.FromUserId == user.Id).Include(x => x.FromUser).OrderByDescending(x => x.MesajTarih).ToListAsync();
                var gelenmesajlar = await c.Messages.Where(x => x.ToUserMessageId == user.Id).Include(x => x.FromUser).OrderByDescending(x => x.MesajTarih).ToListAsync();
                
                var chats = new List<ChatViewModel>();
                foreach (var i in await userManager.Users.Where(x=>x.Id != user.Id).ToListAsync())
                {
                    if (i == user) continue;
                    

                    var chat = new ChatViewModel()
                    {
                        MyMessages = messages.Where(x => x.FromUserId == user.Id && x.ToUserMessageId == i.Id).ToList(),
                        OtherMessages = messages.Where(x => x.FromUserId == i.Id && x.ToUserMessageId == user.Id).ToList(),
                        RecipientName = i.UserName,
                        NameSurname = i.NameSurname,
                        UserImage = i.Image

                    };

                    var chatMessages = new List<Message>();
                    chatMessages.AddRange(chat.MyMessages);
                    chatMessages.AddRange(chat.OtherMessages);

                    chat.LastMessage = chatMessages.OrderByDescending(x => x.MesajTarih).FirstOrDefault();

                    chats.Add(chat);


                }
                return View(chats);
            }
          

            
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string to, string text)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var recipient = await c.Users.SingleOrDefaultAsync(x => x.UserName == to);

            Message message = new Message()
            {
                FromUserId = user.Id,
                ToUserMessageId = recipient.Id,
                Mesaj = text,
                MesajTarih = DateTime.Now
            };
            await c.AddAsync(message);
            await c.SaveChangesAsync();

          

            //string connectionId = ChatHub.UsernameConnectionId[recipient.UserName];
            await hub.Clients.Client(to).SendAsync("RecieveMessage", new Message
            {
                Mesaj=message.Mesaj,
                MesajTarih=DateTime.Now,
                FromUserId=user.Id,
                ToUserMessageId=recipient.Id
            });

            //await hub.Clients.Client(connectionId).SendAsync("RecieveMessage", message.Mesaj, message.MesajTarih.ToShortDateString());

            return RedirectToAction("Index", "Message");
        }
    }
}
