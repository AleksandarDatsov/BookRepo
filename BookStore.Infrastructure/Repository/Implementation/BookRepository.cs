using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.Entities;
using BookStore.Domain.Infrastructure.Repository.Interfaces;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repository.Implementation;

namespace BookStore.Infrastructure.Persistence.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Book> GetAllBooksByAuthor(int authorId)
        {
            return Context.Set<Book>()
                .Where(b => b.AuthorId == authorId)
                .OrderBy(b => b.BookName)
                .ToList();
        }
    }
}