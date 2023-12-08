using EntityLayer.Concreate;

namespace IBlog.Areas.Admin.Models
{
    public class ExcelBlogModel
    {       
        public string BlogBaslik { get; set; }      
        public DateTime OlusturmaTarihi { get; set; }
             
        public string CategoryName { get; set; }

        public string WriterName { get; set; }
        

        
    }
}
