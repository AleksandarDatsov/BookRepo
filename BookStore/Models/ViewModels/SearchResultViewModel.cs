using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel()
        {
            this.Books = new List<BookResultViewModel>();
        }

        public List<BookResultViewModel> Books { get; set; }
    }
}