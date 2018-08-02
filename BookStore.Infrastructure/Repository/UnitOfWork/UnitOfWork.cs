using BookStore.Domain.Infrastructure.Repository.Interfaces;
using BookStore.Domain.Infrastructure.UnitOfWorkPattern;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Persistence.Repositories;

namespace BookStore.Infrastructure.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreDbContext _context;

        public UnitOfWork(BookStoreDbContext context)
        {
            _context = context;
            UsersRepository = new UserRepository(_context);
            BooksRepository = new BookRepository(_context);
            AuthorsRepository = new AuthorRepository(_context);
        }

        public IUserRepository UsersRepository { get; }

        public IBookRepository BooksRepository { get; }

        public IAuthorRepository AuthorsRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}