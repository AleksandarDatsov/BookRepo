using BookStore.Domain.Entities;
using BookStore.Domain.Infrastructure.Repository.Interfaces;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repository.Implementation;

namespace BookStore.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BookStoreDbContext context)
            : base(context)
        {
        }

        public User Find(string username)
        {
            return Context.Set<User>().Find(username);
        }
    }
}