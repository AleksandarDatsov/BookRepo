namespace BookStore.Models.ViewModels
{
    public class BookResultViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string BookName { get; set; }

        public string ReleaseYear { get; set; }

        public bool IsInStock { get; set; }

        public int NumbersInStock { get; set; }
    }
}