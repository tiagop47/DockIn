using DockIn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ArmazemConfig : IEntityTypeConfiguration<Armazem>
{
    public void Configure(EntityTypeBuilder<Armazem> builder)
    {
        builder.ToTable("Armazens");

        builder.HasKey(p => p.ArmazemId);

        builder.Property(p => p.Nome)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Descricao)
               .HasMaxLength(255);

        builder.Property(p => p.Ativo)
               .IsRequired();
    }
}
