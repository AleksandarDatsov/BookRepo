using System.Collections.Generic;
using BookStore.Domain.Enumerations;

namespace BookStore.Domain.Infrastructure.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();

        Author Find(int id);
    }
}