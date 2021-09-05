﻿// <auto-generated />
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DatabaseContext.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210905025811_CreateAllCoins")]
    partial class CreateAllCoins
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DatabaseContext.Models.BinanceUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ApiSecret")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("PortfolioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_BinanceUser_Email");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("Id", "Password")
                        .IsUnique()
                        .HasDatabaseName("IX_BinanceUser_Username_Password");

                    b.ToTable("BinanceUser");
                });

            modelBuilder.Entity("DatabaseContext.Models.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Coin_Id");

                    b.ToTable("Coin");
                });

            modelBuilder.Entity("DatabaseContext.Models.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("Id");

                    b.ToTable("Portfolio");
                });

            modelBuilder.Entity("DatabaseContext.Models.PortfolioCoin", b =>
                {
                    b.Property<int>("PortfolioId")
                        .HasColumnType("integer");

                    b.Property<int>("CoinId")
                        .HasColumnType("integer");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.HasKey("PortfolioId", "CoinId");

                    b.HasIndex("CoinId");

                    b.ToTable("PortfolioCoin");
                });

            modelBuilder.Entity("DatabaseContext.Models.BinanceUser", b =>
                {
                    b.HasOne("DatabaseContext.Models.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("DatabaseContext.Models.PortfolioCoin", b =>
                {
                    b.HasOne("DatabaseContext.Models.Coin", "Coin")
                        .WithMany("Portfolios")
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseContext.Models.Portfolio", "Portfolio")
                        .WithMany("Coins")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coin");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("DatabaseContext.Models.Coin", b =>
                {
                    b.Navigation("Portfolios");
                });

            modelBuilder.Entity("DatabaseContext.Models.Portfolio", b =>
                {
                    b.Navigation("Coins");
                });
#pragma warning restore 612, 618
        }
    }
}
