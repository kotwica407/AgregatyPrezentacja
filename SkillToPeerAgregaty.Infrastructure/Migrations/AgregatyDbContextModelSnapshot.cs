﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkillToPeerAgregaty.Infrastructure.DAL;

#nullable disable

namespace SkillToPeerAgregaty.Infrastructure.Migrations
{
    [DbContext(typeof(AgregatyDbContext))]
    partial class AgregatyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("agregaty")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SkillToPeerAgregaty.Domain.Order.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<DateTime>("CreatedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedUtcDate");

                    b.Property<DateTime?>("DeletedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedUtcDate");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("ModifiedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ModifiedUtcDate");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("Status");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("int")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.ToTable("Order", "agregaty");
                });

            modelBuilder.Entity("SkillToPeerAgregaty.Domain.Order.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("CreatedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedUtcDate");

                    b.Property<DateTime?>("DeletedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedUtcDate");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("ModifiedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ModifiedUtcDate");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrderId");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem", "agregaty");
                });

            modelBuilder.Entity("SkillToPeerAgregaty.Domain.Product.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<decimal>("AvailableAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("AvailableAmount");

                    b.Property<DateTime>("CreatedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedUtcDate");

                    b.Property<DateTime?>("DeletedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedUtcDate");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("ModifiedUtcDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ModifiedUtcDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("int")
                        .HasColumnName("Version");

                    b.HasKey("Id");

                    b.ToTable("Product", "agregaty");
                });

            modelBuilder.Entity("SkillToPeerAgregaty.Infrastructure.DAL.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedUtcDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessedUtcDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("TriesLeft")
                        .HasColumnType("tinyint");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", "agregaty");
                });

            modelBuilder.Entity("SkillToPeerAgregaty.Domain.Order.Entities.OrderItem", b =>
                {
                    b.HasOne("SkillToPeerAgregaty.Domain.Order.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SkillToPeerAgregaty.Domain.Order.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
