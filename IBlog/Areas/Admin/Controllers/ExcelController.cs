using BusinessLayer.Concretae;
using ClosedXML.Excel;
using DataAccessLayer.EntityFaremwork;
using EntityLayer.Concreate;
using ExcelDataReader;
using IBlog.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;

namespace IBlog.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ExcelController : Controller
	{
		BlogManager bm=new BlogManager(new BlogRepository());
		NotificationManager nm=new NotificationManager(new NotificationRepository());
		public IActionResult Index()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (file != null && file.Length > 0)
            {
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Excels\\";

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            bool isHeaderSkipped = false;

                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                Notification notification = new Notification();
                                notification.NotificationType = reader.GetValue(1).ToString();
                                notification.NotificationContent = reader.GetValue(2).ToString();
                                notification.NotificationIcon = reader.GetValue(3).ToString();

                                nm.Insert(notification);                                
                            }
                        } while (reader.NextResult());

                        
                        return RedirectToAction("Index","Home",new { area = "Admin" });
                    }
                }
            }
            return View();
                           
            
        }
        public IActionResult ExcelBlogEvent()
		{
			using (var workbook =new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Bloglar");
				worksheet.Cell(1, 1).Value="Blog Adı";
                worksheet.Cell(1, 2).Value = "Blog Tarihi";
                worksheet.Cell(1, 3).Value = "Blog Kategori";
                worksheet.Cell(1, 4).Value = "Blog Yazarı";

				int blogrowcount = 2;
				foreach(var item in BlogListMethod())
				{
					worksheet.Cell(blogrowcount, 1).Value = item.BlogBaslik;
                    worksheet.Cell(blogrowcount, 2).Value = item.OlusturmaTarihi;
                    worksheet.Cell(blogrowcount, 3).Value = item.CategoryName;
                    worksheet.Cell(blogrowcount, 4).Value = item.WriterName;
					blogrowcount++;
                }
				using (var ms =new MemoryStream())
				{
					workbook.SaveAs(ms);
					var content=ms.ToArray();
					return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Bloglar.xlsx");
				}
            }
			
		}
		public List<ExcelBlogModel> BlogListMethod()
		{
			List<ExcelBlogModel> ebm=new List<ExcelBlogModel>();

			ebm = bm.GetBlogsListWithCategory().Select(x => new ExcelBlogModel
			{
				BlogBaslik = x.BlogBaslik,
				CategoryName = x.Category.KategoriAdi,
				OlusturmaTarihi = x.OlusturmaTarihi,
				WriterName = x.AppUser.NameSurname
			}).ToList();
			return ebm;
		}
	}
}
