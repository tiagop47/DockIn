using DockIn.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");

        builder.HasKey(p => p.StockId);

        builder.HasOne<Artigo>()
        .WithMany()
        .HasForeignKey(a => a.ArtigoId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Property(s => s.Preco)
        .IsRequired();


        builder.Property(s => s.Quantidade)
        .IsRequired();


        builder.Property(s => s.QuantidadeReservada)
        .IsRequired();


        builder.Property(s => s.CreatedAt)
        .IsRequired();

        builder.Property(s => s.Versao)
        .IsConcurrencyToken();
    }
}
