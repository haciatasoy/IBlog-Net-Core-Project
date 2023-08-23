using BusinessLayer.Concretae;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFaremwork;
using DTOLayer.DTOs.Blog;
using DTOLayer.DTOs.Writer;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

namespace IBlog.Controllers
{
    [Authorize(Roles = "Admin,Yazar")]
    public class WriterController : Controller
    {
        Context c = new Context();
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public WriterController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult CreateBlog()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBlog(CreateBlogDto model)
        {

            var username = User.Identity.Name;
            var userid = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            Blog blog = new Blog();
            blog.OlusturmaTarihi = DateTime.Parse(DateTime.Now.ToLongDateString());
            blog.BlogBaslik = model.BlogBaslik;
            blog.BlogIcerik = model.BlogIcerik;
            blog.CategoryId = model.CategoryId;
            blog.AppUserId = userid;
            if (model.BlogResim != null)
            {
                var extension = Path.GetExtension(model.BlogResim.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogPictures/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.BlogResim.CopyTo(stream);
                stream.Close();
                blog.BlogResim = newImageName;
            }
            c.Blogs.Add(blog);
            c.SaveChanges();
            return RedirectToAction("Index", "Dashboard");


            return View(model);
        }
        [HttpPost]
        public IActionResult CategoriSec(string search = "", int page = 1)
        {

            var data = c.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.KategoriAdi.Contains(search) || m.KategoriAdi.Contains(search));
            }
            data = data.OrderBy(m => m.KategoriAdi);


            var list = data.Select(m => new
            {
                id = m.KategoriId,
                text = m.KategoriAdi


            }).Skip((page * 10 - 10)).Take(10).ToList();
            var more = data.Count() > (page * 10);
            return Json(new { results = list, pagination = new { more = more } });
        }
        [HttpGet]
        public async Task<IActionResult> WriterUpdate()
        {
            var writerinfo = await userManager.FindByNameAsync(User.Identity.Name);
            WriterUpdateDto model = new WriterUpdateDto();
            model.NameSurname = writerinfo.NameSurname;
            model.UserName = writerinfo.UserName;
            model.Email = writerinfo.Email;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> WriterUpdate(WriterUpdateDto model)
        {

            var writerinfo = await userManager.FindByNameAsync(User.Identity.Name);
            writerinfo.UserName = model.UserName;
            writerinfo.Email = model.Email;
            writerinfo.NameSurname = model.NameSurname;
            if (model.Image != null)
            {
                if (writerinfo.Image != "https://i.hizliresim.com/645lujq.jpg")
                {
                    string imagedelete = writerinfo.Image;
                    string imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserPictures/", imagedelete);
                    FileInfo fileInfo = new FileInfo(imagepath);

                    if (fileInfo != null)
                    {
                        fileInfo.Delete();
                    }
                }
                var extension = Path.GetExtension(model.Image.FileName);
                var newImageName = writerinfo.NameSurname + Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserPictures/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.Image.CopyTo(stream);
                stream.Close();
                writerinfo.Image = newImageName;
            }
            if (model.Password != null)
            {
                if (model.Password.Length < 8)
                {
                    ViewData["ErrorMessage"] = "Parola en az 8 karakter uzunluğunda olmalıdır!";
                    return View();
                }
                if (!model.Password.Any(char.IsUpper))
                {
                    ViewData["ErrorMessage"] = "Parola en az 1 büyük harf içermelidir!";
                    return View();
                }
                if (!model.Password.Any(char.IsLower))
                {
                    ViewData["ErrorMessage"] = "Parola en az 1 küçük harf içermelidir!";
                    return View();
                }
                if(!model.Password.Any(char.IsPunctuation))
                {
                    ViewData["ErrorMessage"] = "Parola en az 1 noktalama içermelidir!";
                    return View();
                }
                if (!model.Password.Any(char.IsDigit))
                {
                    ViewData["ErrorMessage"] = "Parola en az 1 rakam içermelidir!";
                    return View();
                }
                if(model.Password.CompareTo(model.ConfirmPassword)!=0)
                {
                    ViewData["ErrorMessage"] = "Parolalar uyuşmuyor";
                    return View();
                }
                writerinfo.PasswordHash = userManager.PasswordHasher.HashPassword(writerinfo, model.Password);
            }
            var result = await userManager.UpdateAsync(writerinfo);
            if (result.Succeeded)
            {
                await signInManager.SignOutAsync();

                return RedirectToAction("Index", "Login");
            }
            else
            {
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError("", x.Description);
                }
            }




            return View(model);
        }
    }
}
