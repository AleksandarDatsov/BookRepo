using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BookStore.Domain.Entities;

namespace BookStore.Infrastructure.Data.Maps
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(u => u.Username).IsRequired();

            this.Property(u => u.Password).IsRequired();

            this.Property(u => u.Email).IsRequired();

            this.ToTable("User");
            this.Property(u => u.Username).HasColumnName("Username");
            this.Property(u => u.Password).HasColumnName("Password");
            this.Property(u => u.Age).HasColumnName("Age");
            this.Property(u => u.Email).HasColumnName("Email");
        }
    }
}