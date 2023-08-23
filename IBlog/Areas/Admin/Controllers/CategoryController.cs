using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using DTOLayer.DTOs.Kategori;
using EntityLayer.Concreate;
using IBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace IBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        CategoryManager cat=new CategoryManager(new CategoryRepository());
        BlogManager bm=new BlogManager(new BlogRepository());
        

		public IActionResult Index()
        {
            var values = cat.GetListAll();
            CategoryAddDto model= new CategoryAddDto();
            model.Category = values;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryAddDto model)
        {
            Category category = new Category
            {
                KategoriAdi = model.KategoriAdi,
                Durum=true,
            };
            

            if (model.KategoriResim != null)
            {

                var extension = Path.GetExtension(model.KategoriResim.FileName);
                var newImageName =model.KategoriAdi + Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryPictures/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.KategoriResim.CopyTo(stream);
                stream.Close();
                category.KategoriResim = newImageName;
            }



            cat.Insert(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var editcat=cat.GetById(id);
            CategoryAddDto model=new CategoryAddDto();
            model.KategoriAdi = editcat.KategoriAdi;
            return RedirectToAction("Update",model);
        }
        [HttpPost]
        public IActionResult Update(CategoryAddDto model)
        {
            var editcat = cat.GetById(model.KategoriId);
            editcat.KategoriAdi= model.KategoriAdi;

            if(model.KategoriResim != null)
            {
                if (editcat.KategoriResim != null)
                {
                    string imagedelete = editcat.KategoriResim;
                    string imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryPictures/", imagedelete);
                    FileInfo fileInfo = new FileInfo(imagepath);

                    if (fileInfo != null)
                    {
                        fileInfo.Delete();
                    }
                }
                var extension = Path.GetExtension(model.KategoriResim.FileName);
                var newImageName =editcat.KategoriAdi + Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryPictures/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                model.KategoriResim.CopyTo(stream);
                stream.Close();
                editcat.KategoriResim = newImageName;
            }
            cat.Update(editcat);
            return RedirectToAction("Index");
        }

        public IActionResult getBlogsByCategory(int id)
        {
            var blogs=bm.GetBlogsListWithCategory().Where(x=>x.CategoryId == id).ToList();
            return View(blogs);
        }
    }
}
