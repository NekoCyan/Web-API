using System.ComponentModel.DataAnnotations;

namespace ControllerAPI_1721030861.Models
{
    public class AccountModel : LoginModel
    {
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number length mismatch")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email structure is invalid")]
        public string email { get; set; }
    }
}
