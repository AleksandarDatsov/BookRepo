using System.ComponentModel.DataAnnotations;
using BookStore.Resources;

namespace BookStore.Models
{
    public class OrderViewModel
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string Address { get; set; }
    }
}