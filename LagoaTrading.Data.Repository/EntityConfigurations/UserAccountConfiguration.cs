using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LagoaTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LagoaTrading.Data.Repository.EntityConfigurations
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.CurrencyId)
                .IsRequired();

            builder.Property(x => x.Balance)
                .IsRequired();

            builder.Property(x => x.BalanceAvailable)
                .IsRequired();

            builder.Property(x => x.BalanceBlocked)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserAccount)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Currency)
                .WithMany(x => x.UserAccount)
                .HasForeignKey(x => x.CurrencyId);
        }
    }
}
