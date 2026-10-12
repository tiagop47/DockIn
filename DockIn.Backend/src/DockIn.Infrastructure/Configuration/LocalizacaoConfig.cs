using DockIn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DockIn.Infrastructure.Configuration
{
    public class LocalizacaoConfig : IEntityTypeConfiguration<Localizacao>
    {
        public void Configure(EntityTypeBuilder<Localizacao> builder)
        {
            builder.ToTable("Localizacoes");
            builder.HasKey(u => u.LocalizacaoId);

            builder.Property(u => u.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Tipo)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Ativo)
                   .IsRequired();

            builder.HasOne<Armazem>()
                   .WithMany()
                   .HasForeignKey(u => u.ArmazemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => new { u.ArmazemId, u.Codigo })
                   .IsUnique();
        }
    }
}


