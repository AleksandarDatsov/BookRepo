using System.Data.Entity;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public interface IBookStoreDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
