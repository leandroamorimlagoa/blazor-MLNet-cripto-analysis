using LagoaTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LagoaTrading.Data.Repository.EntityConfigurations
{
    internal class MarketConfiguration : IEntityTypeConfiguration<Market>
    {
        public void Configure(EntityTypeBuilder<Market> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Symbol)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.QuantityMin)
                .IsRequired();

            builder.Property(x => x.QuantityIncrement)
                .IsRequired();

            builder.Property(x => x.PriceMin)
                .IsRequired();

            builder.Property(x => x.PriceIncrement)
                .IsRequired();

            builder.HasOne(x => x.CurrencyBase)
                .WithMany(x => x.MarketBase)
                .HasForeignKey(x => x.CurrencyBaseId);

            builder.HasOne(x => x.CurrencyQuote)
                .WithMany(x => x.MarketQuote)
                .HasForeignKey(x => x.CurrencyQuoteId);

            builder.HasIndex(x => x.Symbol)
                .IsUnique();
        }
    }
}
