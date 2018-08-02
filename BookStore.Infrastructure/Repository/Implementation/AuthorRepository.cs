using BookStore.Domain.Enumerations;
using BookStore.Domain.Infrastructure.Repository.Interfaces;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repository.Implementation;

namespace BookStore.Infrastructure.Persistence.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookStoreDbContext context)
            : base(context)
        {
        }

        public Author Find(int id)
        {
            return Context.Set<Author>().Find(id);
        }
    }
}