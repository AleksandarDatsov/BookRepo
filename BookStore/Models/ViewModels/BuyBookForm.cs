using System.ComponentModel.DataAnnotations;
using BookStore.Resources;

namespace BookStore.Models.ViewModels
{
    public class BuyBookForm
    {
        //Email Sender Name
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "FromNameRequired")]
        [Display(Name = "FromName", ResourceType = typeof(Resources.Languages.Resource))]
        public string FromName { get; set; }

        //Email Sender Email id
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Languages.Resource))]
        public string EmailSender { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "NumbersRequired")]
        [Display(Name = "Numbers", ResourceType = typeof(Resources.Languages.Resource))]
        [Range(1, 30, ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "Numbers")]
        public int Numbers { get; set; }

        // Message body
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "AddressRequired")]
        [Display(Name = "Address", ResourceType = typeof(Resources.Languages.Resource))]
        public string Address { get; set; }
    }
}