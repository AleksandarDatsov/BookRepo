using BookStore.Domain.Infrastructure.Repository.Interfaces;

namespace BookStore.Domain.Infrastructure.UnitOfWorkPattern
{
    public interface IUnitOfWork
    {
        IUserRepository UsersRepository { get; }

        IBookRepository BooksRepository { get; }

        IAuthorRepository AuthorsRepository { get; }

        int Complete();
    }
}