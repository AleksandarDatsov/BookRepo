using System.ComponentModel.DataAnnotations;
using BookStore.Resources;

namespace BookStore.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "UsernameRequired")]
        [Display(Name = "Username", ResourceType = typeof(Resources.Languages.Resource))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "PasswordRequired")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Languages.Resource))]
        public string Password { get; set; }
    }
}