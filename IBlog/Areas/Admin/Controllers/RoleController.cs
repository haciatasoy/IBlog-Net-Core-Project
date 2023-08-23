using DataAccessLayer.Concreate;
using EntityLayer.Concreate;
using IBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using X.PagedList;

namespace IBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        Context c=new Context();

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, Context c)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.c = c;
        }
        public async Task<IActionResult> Index()
        {
            var values=await roleManager.Roles.ToListAsync();
            RoleViewModel model = new RoleViewModel();
            model.Role = values;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            
                AppRole role = new AppRole
                {
                    Name = model.RoleName
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                return RedirectToAction("Index");
            }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            
           
            return View();
        }
        [HttpGet]
        public IActionResult Update(int RoleId)
        {
            var value = roleManager.Roles.Where(x => x.Id == RoleId).FirstOrDefault();
            RoleViewModel model=new RoleViewModel();
            model.RoleName = value.Name;
            return RedirectToAction("Update",model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoleViewModel model)
        {
            var value = roleManager.Roles.Where(x=>x.Id==model.RoleId).FirstOrDefault();
            value.Name=model.RoleName;
            var result = await roleManager.UpdateAsync(value);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var value=roleManager.Roles.Where(x=>x.Id == id).FirstOrDefault();
            if (value != null)
            {
                roleManager.DeleteAsync(value);
                return RedirectToAction("Index");
            }
            return View() ;
        }
        public IActionResult UserList()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = userManager.Users.Where(x=>x.Id == id).FirstOrDefault();
            var roles=roleManager.Roles.ToList();

            TempData["UserId"] = user.Id;

            var userRoles = await userManager.GetRolesAsync(user);
            List<RoleAssignViewModel> model = new List<RoleAssignViewModel>();
            foreach (var item in roles)
            {
                RoleAssignViewModel m = new RoleAssignViewModel();
                m.RoleID = item.Id;
                m.Name = item.Name;
                m.Exists = userRoles.Contains(item.Name);
                model.Add(m);
            }
            return View(model);
            
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model)
        {
            var userid = (int)TempData["UserId"];
            var user = userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach (var item in model)
            {
                if (item.Exists)
                {
                    await userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("UserList");
            
        }
    }
}
