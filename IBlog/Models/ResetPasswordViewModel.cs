using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IBlog.Models
{
    public class ResetPasswordViewModel
    {
        public int code { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(8)]
        [RegularExpression("(?=[A-Za-z0-9@#$%^&+!=<>',.]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=<>',.])(?=.{8,}).*$", ErrorMessage = "Password must contain at least one upper letter,lower letter,number and punctuation!")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmpassword { get; set; }
    }
}
