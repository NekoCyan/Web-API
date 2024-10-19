using System.ComponentModel.DataAnnotations;

namespace ControllerAPI_1721030861.Models
{
    public class AccountModel : LoginModel
    {
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "**")]
        public string phone { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "**")]
        public string email { get; set; }
    }
}
