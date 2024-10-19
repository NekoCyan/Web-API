using System.ComponentModel.DataAnnotations;

namespace ControllerAPI_1721030861.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_]{3,10}$", ErrorMessage = "Username structure is invalid")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\w])[\S]{8,}$", ErrorMessage = "Password too weak")]
        public string password { get; set; }
    }
}
