using LagoaTrading.Domain.Core.Securities;
using LagoaTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LagoaTrading.Data.Repository.EntityConfigurations
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.ApiKey)
                .HasMaxLength(ConstantNamesServer.Database.HashSize)
                .IsRequired();

            builder.Property(x => x.ApiSecret)
                .HasMaxLength(ConstantNamesServer.Database.HashSize)
                .IsRequired();

            builder.Property(x => x.OnlyPositiveCryptos)
                .IsRequired();

            builder.Property(x => x.TypeValue)
                .IsRequired();

            builder.Property(x => x.ReferenceValue)
                .IsRequired();

            builder.Property(x => x.ReferenceAbsoluteValue)
                .IsRequired();

            builder.Property(x => x.MinimumCryptoValue)
                .IsRequired();

            builder.Property(x => x.MaximumCryptoValue)
                .IsRequired();

            builder.Property(x => x.PercentageToDecreaseToBuy)
                .IsRequired();

            builder.Property(x => x.PercentageToIncreaseToSell)
                .IsRequired();

            builder.HasIndex(x => x.UserId)
                .IsUnique();
        }
    }
}
