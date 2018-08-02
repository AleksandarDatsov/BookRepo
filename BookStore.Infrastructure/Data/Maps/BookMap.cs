using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data.Maps
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(u => u.BookName).IsRequired();

            this.Property(u => u.ReleaseYear).IsRequired();

            this.Property(u => u.AuthorId).IsRequired();

            this.ToTable("Book");
            this.Property(b => b.AuthorId).HasColumnName("AuthorId");
            this.Property(b => b.BookName).HasColumnName("BookName");
            this.Property(b => b.ReleaseYear).HasColumnName("ReleaseYear");
            this.Property(b => b.IsInStock).HasColumnName("IsInStock");
            this.Property(b => b.NumbersInStock).HasColumnName("NumbersInStock");

            this.HasRequired(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorId);
        }
    }
}