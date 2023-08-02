using LagoaTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LagoaTrading.Data.Repository.EntityConfigurations
{
    public class CircuitConfiguration : IEntityTypeConfiguration<Circuit>
    {
        public void Configure(EntityTypeBuilder<Circuit> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.CircuitType)
                .IsRequired();

            builder.Property(x => x.StartDateTime)
                .IsRequired();

            builder.Property(x => x.EndDateTime)
                .IsRequired(false);

            builder.Property(x => x.StartValue)
                .IsRequired();

            builder.Property(x => x.EndValue)
                .IsRequired();

            builder.Property(x => x.DifferenceValue)
                .IsRequired();

            builder.Ignore(x => x.PositionBuy);

            builder.Ignore(x => x.PositionSell);

            builder.HasMany(x => x.Positions)
                .WithOne(x => x.Circuit)
                .HasForeignKey(x => x.CircuitId);
        }
    }
}
