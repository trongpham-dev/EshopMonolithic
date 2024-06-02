﻿// <auto-generated />
using System;
using EshopMonolithic.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EshopMonolithic.Infrastructure.Migrations
{
    [DbContext(typeof(OrderingContext))]
    [Migration("20240601125907_AddCatalog")]
    partial class AddCatalog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ordering")
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("buyerseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderitemseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("paymentseq")
                .IncrementsBy(10);

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "buyerseq");

                    b.Property<string>("IdentityGuid")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdentityGuid")
                        .IsUnique();

                    b.ToTable("buyers", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.CardType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("cardtypes", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "paymentseq");

                    b.Property<int>("BuyerId")
                        .HasColumnType("integer");

                    b.Property<string>("_alias")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("Alias");

                    b.Property<string>("_cardHolderName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("CardHolderName");

                    b.Property<string>("_cardNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("CardNumber");

                    b.Property<int>("_cardTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("CardTypeId");

                    b.Property<DateTime>("_expiration")
                        .HasMaxLength(25)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Expiration");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("_cardTypeId");

                    b.ToTable("paymentmethods", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate.CatalogBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("CatalogBrand", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableStock")
                        .HasColumnType("integer");

                    b.Property<int>("CatalogBrandId")
                        .HasColumnType("integer");

                    b.Property<int>("CatalogTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxStockThreshold")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("OnReorder")
                        .HasColumnType("boolean");

                    b.Property<string>("PictureFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("RestockThreshold")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CatalogBrandId");

                    b.HasIndex("CatalogTypeId");

                    b.HasIndex("Name");

                    b.ToTable("Catalog", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate.CatalogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("CatalogType", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderseq");

                    b.Property<int?>("BuyerId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("integer")
                        .HasColumnName("PaymentMethodId");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("PaymentId");

                    b.ToTable("orders", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderitemseq");

                    b.Property<decimal>("Discount")
                        .HasColumnType("numeric");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("Units")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderItems", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("requests", "ordering");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", b =>
                {
                    b.HasOne("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.Buyer", null)
                        .WithMany("PaymentMethods")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.CardType", "CardType")
                        .WithMany()
                        .HasForeignKey("_cardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardType");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate.CatalogItem", b =>
                {
                    b.HasOne("EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate.CatalogBrand", "CatalogBrand")
                        .WithMany()
                        .HasForeignKey("CatalogBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EshopMonolithic.Domain.AggregatesModel.CatalogAggegrate.CatalogType", "CatalogType")
                        .WithMany()
                        .HasForeignKey("CatalogTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogBrand");

                    b.Navigation("CatalogType");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.HasOne("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.Buyer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.PaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("EshopMonolithic.Domain.AggregatesModel.OrderAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("integer");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders", "ordering");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("EshopMonolithic.Domain.AggregatesModel.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.BuyerAggregate.Buyer", b =>
                {
                    b.Navigation("PaymentMethods");
                });

            modelBuilder.Entity("EshopMonolithic.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
