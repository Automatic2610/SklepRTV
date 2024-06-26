﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SklepRTV.Database;

#nullable disable

namespace SklepRTV.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SklepRTV.Model.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("countryId")
                        .HasColumnType("int");

                    b.Property<int>("flatNo")
                        .HasColumnType("int");

                    b.Property<int>("houseNo")
                        .HasColumnType("int");

                    b.Property<string>("province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("SklepRTV.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SklepRTV.Model.Country", b =>
                {
                    b.Property<int>("idCountry")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCountry"));

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCountry");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("SklepRTV.Model.Customer", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SklepRTV.Model.JobPosition", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("JobPositions");
                });

            modelBuilder.Entity("SklepRTV.Model.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("customerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SklepRTV.Model.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.Property<int>("unitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SklepRTV.Model.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("branchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("SklepRTV.Model.Worker", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("SklepRTV.Model.Category", b =>
                {
                    b.HasOne("SklepRTV.Model.Product", null)
                        .WithMany("Categories")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("SklepRTV.Model.Customer", b =>
                {
                    b.OwnsOne("SklepRTV.Model.AddressDetails", "addressDetails", b1 =>
                        {
                            b1.Property<Guid>("Customerid")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("city")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("countryId")
                                .HasColumnType("int");

                            b1.Property<int>("flatNo")
                                .HasColumnType("int");

                            b1.Property<int>("houseNo")
                                .HasColumnType("int");

                            b1.Property<string>("province")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Customerid");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("Customerid");
                        });

                    b.OwnsOne("SklepRTV.Model.ContactDetails", "contactDetails", b1 =>
                        {
                            b1.Property<Guid>("Customerid")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Customerid");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("Customerid");
                        });

                    b.Navigation("addressDetails")
                        .IsRequired();

                    b.Navigation("contactDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("SklepRTV.Model.Product", b =>
                {
                    b.HasOne("SklepRTV.Model.Order", null)
                        .WithMany("products")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("SklepRTV.Model.Warehouse", b =>
                {
                    b.HasOne("SklepRTV.Model.Product", null)
                        .WithMany("warehouses")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("SklepRTV.Model.Worker", b =>
                {
                    b.OwnsOne("SklepRTV.Model.AddressDetails", "addressDetails", b1 =>
                        {
                            b1.Property<Guid>("Workerid")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("city")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("countryId")
                                .HasColumnType("int");

                            b1.Property<int>("flatNo")
                                .HasColumnType("int");

                            b1.Property<int>("houseNo")
                                .HasColumnType("int");

                            b1.Property<string>("province")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Workerid");

                            b1.ToTable("Workers");

                            b1.WithOwner()
                                .HasForeignKey("Workerid");
                        });

                    b.OwnsOne("SklepRTV.Model.ContactDetails", "contactDetails", b1 =>
                        {
                            b1.Property<Guid>("Workerid")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Workerid");

                            b1.ToTable("Workers");

                            b1.WithOwner()
                                .HasForeignKey("Workerid");
                        });

                    b.Navigation("addressDetails")
                        .IsRequired();

                    b.Navigation("contactDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("SklepRTV.Model.Order", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("SklepRTV.Model.Product", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("warehouses");
                });
#pragma warning restore 612, 618
        }
    }
}
