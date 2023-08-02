using LagoaTrading.Domain.Core.Securities;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.Enumerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LagoaTrading.Data.Repository.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ConstantNamesServer.Database.NameSize);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(ConstantNamesServer.Database.NameSize);

            builder.Property(x => x.EmailHash)
                .IsRequired()
                .HasMaxLength(ConstantNamesServer.Database.HashSize);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(ConstantNamesServer.Database.HashSize);

            builder.Property(x => x.RollingHash)
                .IsRequired()
                .HasMaxLength(ConstantNamesServer.Database.HashSize);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasQueryFilter(x => x.Status == UserStatus.Active);

            builder.HasOne(x => x.Parameter)
                .WithOne(x => x.User)
                .HasForeignKey<Parameter>(x => x.UserId);
        }
    }
}
