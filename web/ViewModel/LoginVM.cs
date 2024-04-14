
using System.ComponentModel.DataAnnotations;


namespace Web.ViewModel
{
    public class LoginVM
    {
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string RedirectUrl { get; set; }
    }
}