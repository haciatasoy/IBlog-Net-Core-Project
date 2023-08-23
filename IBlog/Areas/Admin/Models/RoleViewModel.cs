using EntityLayer.Concreate;

namespace IBlog.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<AppRole> Role { get; set; }
    }
}
