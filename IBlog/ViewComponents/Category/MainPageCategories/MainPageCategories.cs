using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.ViewComponents.Category.MainPageCategories
{
    public class MainPageCategories : ViewComponent
    {
        CategoryManager cm = new CategoryManager(new CategoryRepository());
        public IViewComponentResult Invoke()
        {
            var categories = cm.GetListAll();
            return View(categories);
        }
    }
}
