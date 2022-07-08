﻿// <auto-generated />
using System;
using Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Basket.Infrastructure.Migrations
{
    [DbContext(typeof(BasketDBContext))]
    partial class BasketDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Basket.Domain.Entities.BasketItemNS.BasketItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("CurrentPrice");

                    b.Property<string>("ItemHashCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ItemHashCode");

                    b.Property<decimal>("OldPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("OldPrice");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ProductName");

                    b.HasKey("Id");

                    b.ToTable("basketItems");
                });

            modelBuilder.Entity("Basket.Domain.Entities.CustomerBasketNS.CustomerBasket", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("CustomerId");

                    b.ToTable("customerBaskets");
                });

            modelBuilder.Entity("BasketItemCustomerBasket", b =>
                {
                    b.Property<Guid>("ItemsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("customerBasketsCustomerId")
                        .HasColumnType("uuid");

                    b.HasKey("ItemsId", "customerBasketsCustomerId");

                    b.HasIndex("customerBasketsCustomerId");

                    b.ToTable("PotentialPurchases", (string)null);
                });

            modelBuilder.Entity("BasketItemCustomerBasket", b =>
                {
                    b.HasOne("Basket.Domain.Entities.BasketItemNS.BasketItem", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Basket.Domain.Entities.CustomerBasketNS.CustomerBasket", null)
                        .WithMany()
                        .HasForeignKey("customerBasketsCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
