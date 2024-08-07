﻿// <auto-generated />
using Dummy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dummy.Domain.Migrations
{
    [DbContext(typeof(DummyContext))]
    partial class DummyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dummy.Domain.Classes.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("companies", (string)null);
                });

            modelBuilder.Entity("Dummy.Domain.Classes.DummyClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DummyDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("DummyPrice")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Dummy_Table", (string)null);
                });

            modelBuilder.Entity("Dummy.Domain.Classes.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Dummy.Domain.Classes.ProductCompanies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ProductId");

                    b.ToTable("products_companies", (string)null);
                });

            modelBuilder.Entity("Dummy.Domain.Classes.ProductCompanies", b =>
                {
                    b.HasOne("Dummy.Domain.Classes.Company", "Companies")
                        .WithMany("ProductsCompanies")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dummy.Domain.Classes.Product", "Products")
                        .WithMany("ProductsCompanies")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Companies");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Dummy.Domain.Classes.Company", b =>
                {
                    b.Navigation("ProductsCompanies");
                });

            modelBuilder.Entity("Dummy.Domain.Classes.Product", b =>
                {
                    b.Navigation("ProductsCompanies");
                });
#pragma warning restore 612, 618
        }
    }
}
