using System.ComponentModel.DataAnnotations;

namespace Purrfect_Blog_Starter.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}