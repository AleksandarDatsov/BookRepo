using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Username", ResourceType = typeof(Resources.Languages.Resource))]
        public string UserName { get; set; }
    }
}