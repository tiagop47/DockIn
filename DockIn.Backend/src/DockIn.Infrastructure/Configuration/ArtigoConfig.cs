using DockIn.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ArtigoConfig : IEntityTypeConfiguration<Artigo>
{
    public void Configure(EntityTypeBuilder<Artigo> builder)
    {
        builder.HasKey(a => a.ArtigoId);

        builder.Property(a => a.Peso)
               .IsRequired();

        builder.Property(a => a.Dimensoes)
               .IsRequired();

        builder.Property(a => a.AtivoBool)
               .IsRequired();

        builder.Property(a => a.CreatedAt)
               .IsRequired();
    }
}
