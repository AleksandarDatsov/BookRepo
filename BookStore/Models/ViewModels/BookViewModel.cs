using System;
using System.ComponentModel.DataAnnotations;
using BookStore.Resources;

namespace BookStore.Models.ViewModels
{
    public class BookViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "BookNameRequired")]
        [StringLength(20, MinimumLength = 4, ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "BookName")]
        [Display(Name = "BookName", ResourceType = typeof(Resources.Languages.Resource))]
        public string BookName { get; set; }

        [Display(Name = "Author", ResourceType = typeof(Resources.Languages.Resource))]
        public int AuthorId { get; set; }

        [Display(Name = "ReleaseYear", ResourceType = typeof(Resources.Languages.Resource))]
        public DateTime ReleaseYear { get; set; }

        [Display(Name = "IsInStock", ResourceType = typeof(Resources.Languages.Resource))]
        public bool IsInStock { get; set; }

        [Range(0, 30, ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "NumbersInStock")]
        [Display(Name = "NumbersInStock", ResourceType = typeof(Resources.Languages.Resource))]
        public int NumbersInStock { get; set; }
    }
}