using DockIn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DockIn.Infrastructure.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(u => u.Ativo);
            builder.Property(u => u.PasswordHash)
          .IsRequired();
            builder.Property(u => u.CreatedAt)
                .IsRequired();
        }
    }
}
