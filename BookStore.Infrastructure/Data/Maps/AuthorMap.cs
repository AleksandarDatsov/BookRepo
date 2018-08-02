
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BookStore.Domain.Enumerations;

namespace BookStore.Infrastructure.Data.Maps
{
    public class AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            this.HasKey(a => a.Id);

            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(a => a.FirstName).IsRequired();

            this.Property(a => a.LastName).IsRequired();

            Ignore(a => a.FullName);

            this.ToTable("Author");
            this.Property(a => a.Id).HasColumnName("Id");
            this.Property(a => a.FirstName).HasColumnName("FirstName");
            this.Property(a => a.LastName).HasColumnName("LastName");
        }
    }
}