using System.ComponentModel.DataAnnotations;
using BookStore.Resources;

namespace BookStore.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "UsernameRequired")]
        [StringLength(20, MinimumLength = 4, ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "Username")]
        [Display(Name = "Username", ResourceType = typeof(Resources.Languages.Resource))]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Languages.Resource))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "ConfirmPasswordRequired")]
        [Compare("Password", ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "ConfirmPassword")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Languages.Resource))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "AgeRequired")]
        [Range(12,99,ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "Age")]
        [Display(Name = "Age", ResourceType = typeof(Resources.Languages.Resource))]
        public int Age { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "Email")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Languages.Resource))]
        public string Email { get; set; }
    }
}