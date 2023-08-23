using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concreate
{
    public class Context:IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Message>()
                 .HasOne(x => x.FromUser)
                 .WithMany(y => y.Sender)
                 .HasForeignKey(z => z.FromUserId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message>()
               .HasOne(x => x.ToUserMessage)
               .WithMany(y => y.Receiver)
               .HasForeignKey(z => z.ToUserMessageId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Contact> Contacts { get; set; }  
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }
        public DbSet<PasswordForgotMail> PasswordForgotMails { get; set; }
        public DbSet<Message>  Messages { get; set; }

       

    }
}
