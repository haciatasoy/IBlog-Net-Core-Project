using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.Areas.Admin.ViewComponents.CategoriesForAdmin
{
    public class CategoryForAdmin:ViewComponent
    {
        CategoryManager cm = new CategoryManager(new CategoryRepository());
        public IViewComponentResult Invoke()
        {
            var kategoris = cm.GetListAll();
            return View(kategoris);
        }
    }
}
