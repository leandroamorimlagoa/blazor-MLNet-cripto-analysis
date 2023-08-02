using LagoaTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LagoaTrading.Data.Repository.EntityConfigurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Side).IsRequired();

            builder.Property(x => x.OrderType).IsRequired();

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.MarketId).IsRequired();

            builder.Property(x => x.ClientOrderId).IsRequired(false);

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.UnitPrice).IsRequired();

            builder.Property(x => x.State).IsRequired();

            builder.Property(x => x.QuantityExecuted).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.Created).IsRequired(false);

            builder.Property(x => x.Executed).IsRequired(false);

            builder.Property(x => x.CircuitId).IsRequired(false);

            builder.Property(x => x.OrderId).IsRequired();

            builder.Property(x => x.Identifier).IsRequired(false);
        }
    }
}
