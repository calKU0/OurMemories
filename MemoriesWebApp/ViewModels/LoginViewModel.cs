using System.ComponentModel.DataAnnotations;

namespace MemoriesWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email adress is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}