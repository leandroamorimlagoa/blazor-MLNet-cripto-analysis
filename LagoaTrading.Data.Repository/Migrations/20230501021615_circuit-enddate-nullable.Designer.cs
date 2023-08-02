﻿// <auto-generated />
using System;
using LagoaTrading.Data.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LagoaTrading.Data.Repository.Migrations
{
    [DbContext(typeof(LagoaTradingContext))]
    [Migration("20230501021615_circuit-enddate-nullable")]
    partial class circuitenddatenullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Candlestick", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("DateTimeUTC")
                        .HasColumnType("bigint");

                    b.Property<long>("MarketId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PriceClose")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PriceHighest")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PriceLowest")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PriceOpen")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Volume")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("MarketId");

                    b.ToTable("Candlestick");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Circuit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<short>("CircuitType")
                        .HasColumnType("smallint");

                    b.Property<decimal>("DifferenceValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("EndValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("StartValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Circuit");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Currency", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Precision")
                        .HasColumnType("int");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Symbol")
                        .IsUnique();

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Invite", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<long?>("FromUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<long?>("ToUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UsedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("FromUserId");

                    b.ToTable("Invite");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Market", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CurrencyBaseId")
                        .HasColumnType("bigint");

                    b.Property<long>("CurrencyQuoteId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PriceIncrement")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PriceMin")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("QuantityIncrement")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("QuantityMin")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyBaseId");

                    b.HasIndex("CurrencyQuoteId");

                    b.HasIndex("Symbol")
                        .IsUnique();

                    b.ToTable("Market");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Parameter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("AccountBalanceCurrency")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("ApiSecret")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<decimal>("AvaliableValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<short>("CircuitCommand")
                        .HasColumnType("smallint");

                    b.Property<decimal>("MaximumCryptoValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("MinimumCryptoValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("OnlyPositiveCryptos")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("PercentageToDecreaseToBuy")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PercentageToIncreaseToSell")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ReferenceAbsoluteValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ReferenceValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TypeValue")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Parameter");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Position", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CircuitId")
                        .HasColumnType("bigint");

                    b.Property<string>("ClientOrderId")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Executed")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("MarketId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<short>("OrderType")
                        .HasColumnType("smallint");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("QuantityExecuted")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ResponsePrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ResponsePriceAVG")
                        .HasColumnType("decimal(65,30)");

                    b.Property<short>("Side")
                        .HasColumnType("smallint");

                    b.Property<short>("State")
                        .HasColumnType("smallint");

                    b.Property<int>("TradeCounts")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CircuitId");

                    b.HasIndex("MarketId");

                    b.HasIndex("UserId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.SyncControl", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastSync")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SyncControl");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("EmailHash")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("RollingHash")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.UserAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("BalanceAvailable")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("BalanceBlocked")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long>("CurrencyId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccount");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Candlestick", b =>
                {
                    b.HasOne("LagoaTrading.Domain.Entities.Market", "Market")
                        .WithMany()
                        .HasForeignKey("MarketId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Market");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Invite", b =>
                {
                    b.HasOne("LagoaTrading.Domain.Entities.User", "FromUser")
                        .WithMany("InviteUsers")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("FromUser");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Market", b =>
                {
                    b.HasOne("LagoaTrading.Domain.Entities.Currency", "CurrencyBase")
                        .WithMany("MarketBase")
                        .HasForeignKey("CurrencyBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LagoaTrading.Domain.Entities.Currency", "CurrencyQuote")
                        .WithMany("MarketQuote")
                        .HasForeignKey("CurrencyQuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrencyBase");

                    b.Navigation("CurrencyQuote");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Parameter", b =>
                {
                    b.HasOne("LagoaTrading.Domain.Entities.User", "User")
                        .WithOne("Parameter")
                        .HasForeignKey("LagoaTrading.Domain.Entities.Parameter", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Position", b =>
                {
                    b.HasOne("LagoaTrading.Domain.Entities.Circuit", "Circuit")
                        .WithMany("Positions")
                        .HasForeignKey("CircuitId");

                    b.HasOne("LagoaTrading.Domain.Entities.Market", "Market")
                        .WithMany()
                        .HasForeignKey("MarketId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LagoaTrading.Domain.Entities.User", "User")
                        .WithMany("Position")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Circuit");

                    b.Navigation("Market");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.UserAccount", b =>
                {
                    b.HasOne("LagoaTrading.Domain.Entities.Currency", "Currency")
                        .WithMany("UserAccount")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LagoaTrading.Domain.Entities.User", "User")
                        .WithMany("UserAccount")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Circuit", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.Currency", b =>
                {
                    b.Navigation("MarketBase");

                    b.Navigation("MarketQuote");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("LagoaTrading.Domain.Entities.User", b =>
                {
                    b.Navigation("InviteUsers");

                    b.Navigation("Parameter")
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("UserAccount");
                });
#pragma warning restore 612, 618
        }
    }
}
