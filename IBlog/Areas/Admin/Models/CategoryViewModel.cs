using EntityLayer.Concreate;

namespace IBlog.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public Category category { get; set; }
        public string categoryname { get; set; }
        public int categorycount { get; set; }
    }
}
