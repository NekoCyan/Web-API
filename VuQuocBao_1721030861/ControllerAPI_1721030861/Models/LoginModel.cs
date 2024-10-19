using System.ComponentModel.DataAnnotations;

namespace ControllerAPI_1721030861.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^[a-zA-Z0-9_]{3,10}$", ErrorMessage = "**")]
        public string userName { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\w])[\S]{8,}$", ErrorMessage = "**")]
        public string password { get; set; }
    }
}
