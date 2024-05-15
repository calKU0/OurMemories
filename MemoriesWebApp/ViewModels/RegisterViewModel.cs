using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MemoriesWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
        public List<IdentityError>? Errors { get; set; }
    }
}
