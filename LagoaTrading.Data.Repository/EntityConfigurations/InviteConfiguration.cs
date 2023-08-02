using LagoaTrading.Domain.Core.Securities;
using LagoaTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LagoaTrading.Data.Repository.EntityConfigurations
{
    public class InviteConfiguration : IEntityTypeConfiguration<Invite>
    {

        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(ConstantNamesServer.Database.NameSize)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(ConstantNamesServer.Database.NameSize)
                .IsRequired();

            builder.Property(x => x.Code)
                .HasMaxLength(ConstantNamesServer.Database.HashSize)
                .IsRequired();

            builder.Property(x => x.FromUserId)
                .IsRequired(false);

            builder.Property(x => x.ToUserId)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.HasOne(x => x.FromUser)
                   .WithMany(x => x.InviteUsers)
                   .HasForeignKey(x => x.FromUserId);
        }
    }
}
