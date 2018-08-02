using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Entities;
using BookStore.Resources;

namespace BookStore.Models.ViewModels
{
    public class HomeViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "BookName")]
        [Display(Name = "BookName", ResourceType = typeof(Resources.Languages.Resource))]
        public string BookName { get; set; }
        
        public virtual IEnumerable<Book> Books { get; set; }

        [Display(Name = "Author", ResourceType = typeof(Resources.Languages.Resource))]
        public int SelectedAuthorId { get; set; }
        
        [Display(Name = "IsInStock", ResourceType = typeof(Resources.Languages.Resource))]
        public bool IsInStock { get; set; }
    }
}