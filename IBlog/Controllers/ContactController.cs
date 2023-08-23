using AutoMapper;
using BusinessLayer.Concretae;
using DataAccessLayer.EntityFaremwork;
using DTOLayer.DTOs.Contact;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBlog.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        ContactManager cm=new ContactManager(new ContactRepository());
        private readonly IMapper mapper;

		public ContactController(IMapper mapper)
		{
			this.mapper = mapper;
		}

		[HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(AddContactDto model)
        {
            var addcontact = mapper.Map<Contact>(model);
            cm.Insert(addcontact);
            return RedirectToAction("Index","Blog");
        }
       
    }
}
