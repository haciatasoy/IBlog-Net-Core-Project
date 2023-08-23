using EntityLayer.Concreate;

namespace IBlog.Areas.Admin.Models
{
    public class AboutViewModel
    {
        public int AboutId { get; set; }
        public string Mail { get; set; }
        public string Adres { get; set; }
        public string MapLocation { get; set; }
        public string Telefon { get; set; }

        public List<About> About { get; set; }
    }
}
