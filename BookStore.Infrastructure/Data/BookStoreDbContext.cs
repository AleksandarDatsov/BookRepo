using System.Data.Entity;
using BookStore.Domain.Entities;
using BookStore.Domain.Enumerations;
using BookStore.Infrastructure.Data.Maps;

namespace BookStore.Infrastructure.Data
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {

        public BookStoreDbContext()
            : base("name=BookStoreDBEntities")
        {
        }

        public static BookStoreDbContext Create()
        {
            return new BookStoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new AuthorMap());
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Author> Authors { get; set; }
    }
}