using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ArmazemConfig : IEntityTypeConfiguration<Armazem>
{
    public void Configure(EntityTypeBuilder<Armazem> builder)
    {
        builder.ToTable("Armazens");

        builder.HasKey(p => p.ArmazemId);

        builder.Property(p => p.Localizacao)
               .IsRequired();

        builder.Property(p => p.CapacidadeMaxima)
               .IsRequired();

        builder.HasMany(p => p.Stock)
        .WithOne()
        .HasForeignKey(a => a.ArmazemId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
