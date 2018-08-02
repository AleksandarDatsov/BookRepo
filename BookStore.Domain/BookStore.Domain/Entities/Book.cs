using BookStore.Domain.Enumerations;
using System;

namespace BookStore.Domain.Entities
{
    public class Book
    {
        public Book(string bookName, int authorId, string releaseYear, bool isInStock, int numbersInStock)
        {
            this.BookName = bookName;
            this.ReleaseYear = releaseYear;
            this.IsInStock = isInStock;
            this.NumbersInStock = numbersInStock;
            this.AuthorId = authorId;
        }

        public Book()
        {}

        public int Id { get; private set; }

        public Author Author { get; private set; }

        public int AuthorId { get; set; }

        public string BookName { get; private set; }

        public string ReleaseYear { get; private set; }

        public bool IsInStock { get; private set; }

        public int NumbersInStock { get; private set; }

        public decimal? Price { get; set; }
    }
}