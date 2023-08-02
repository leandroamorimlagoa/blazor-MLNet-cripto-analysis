using LagoaTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LagoaTrading.Data.Repository.EntityConfigurations
{
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Symbol)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Precision)
                .IsRequired();

            builder.HasIndex(x => x.Symbol)
                .IsUnique();

            builder.HasIndex(x => x.Name)
                            .IsUnique();

        }
    }
}
