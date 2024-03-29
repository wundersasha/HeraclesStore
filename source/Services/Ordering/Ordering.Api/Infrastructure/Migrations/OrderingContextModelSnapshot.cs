﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ordering.Infrastructure;

#nullable disable

namespace Ordering.Api.Infrastructure.Migrations
{
    [DbContext(typeof(OrderingContext))]
    partial class OrderingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("buyerseq", "ordering")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderitemseq", "ordering")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderseq", "ordering")
                .IncrementsBy(10);

            modelBuilder.HasSequence("paymentseq", "ordering")
                .IncrementsBy(10);

            modelBuilder.Entity("Ordering.Domain.Models.BuyerAggregate.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "buyerseq", "ordering");

                    b.Property<Guid>("IdentityId")
                        .HasMaxLength(1000)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("buyers", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.Models.BuyerAggregate.CardType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("cardtypes", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.Models.BuyerAggregate.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "paymentseq", "ordering");

                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("alias")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Alias");

                    b.Property<string>("cardHolderName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("CardHolderName");

                    b.Property<string>("cardNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("CardNumber");

                    b.Property<int>("cardTypeId")
                        .HasColumnType("int")
                        .HasColumnName("CardTypeId");

                    b.Property<DateTime>("expiration")
                        .HasMaxLength(25)
                        .HasColumnType("datetime2")
                        .HasColumnName("Expiration");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("cardTypeId");

                    b.ToTable("paymentmethods", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.Models.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderseq", "ordering");

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int")
                        .HasColumnName("BuyerId");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentMethodId");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<DateTime>("orderDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("OrderDate");

                    b.Property<int>("orderStatusId")
                        .HasColumnType("int")
                        .HasColumnName("OrderStatusId");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("orderStatusId");

                    b.ToTable("orders", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.Models.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderitemseq", "ordering");

                    b.Property<decimal>("Discount")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)")
                        .HasDefaultValue(0m)
                        .HasColumnName("Discount");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PictureUrl");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProductName");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("UnitPrice");

                    b.Property<int>("Units")
                        .HasColumnType("int")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderitems", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.Models.OrderAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("orderstatus", "ordering");
                });

            modelBuilder.Entity("Ordering.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("requests", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.Models.BuyerAggregate.PaymentMethod", b =>
                {
                    b.HasOne("Ordering.Domain.Models.BuyerAggregate.Buyer", null)
                        .WithMany("PaymentMethods")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ordering.Domain.Models.BuyerAggregate.CardType", "CardType")
                        .WithMany()
                        .HasForeignKey("cardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardType");
                });

            modelBuilder.Entity("Ordering.Domain.Models.OrderAggregate.Order", b =>
                {
                    b.HasOne("Ordering.Domain.Models.BuyerAggregate.Buyer", null)
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("Ordering.Domain.Models.BuyerAggregate.PaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ordering.Domain.Models.OrderAggregate.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("orderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Ordering.Domain.Models.OrderAggregate.Order.Address#Ordering.Domain.Models.OrderAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseHiLo(b1.Property<int>("OrderId"), "orderseq", "ordering");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderId");

                            b1.ToTable("orders", "ordering");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("Ordering.Domain.Models.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("Ordering.Domain.Models.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Domain.Models.BuyerAggregate.Buyer", b =>
                {
                    b.Navigation("PaymentMethods");
                });

            modelBuilder.Entity("Ordering.Domain.Models.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
