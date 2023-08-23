
using DataAccessLayer.Concreate;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace IBlog.Hubs
{
    public class ChatHub:Hub
    {
        Context c=new Context();
        public static Dictionary<string, string> UsernameConnectionId = new Dictionary<string, string>();

        public ChatHub(Context c)
        {
            this.c = c;
        }

        public async Task<string> GetConnectionId(string username)
        {
            var user = await c.Users.SingleOrDefaultAsync(x => x.UserName == username);

            if (UsernameConnectionId.ContainsKey(username))
            {
                UsernameConnectionId[username] = Context.ConnectionId;
            }
            else
            {
                UsernameConnectionId.Add(username, Context.ConnectionId);
            }

            return Context.ConnectionId;
        }
     
    }
}
