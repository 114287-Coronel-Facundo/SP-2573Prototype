using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dummy.Core.Model.Classes;

namespace Dummy.Core.Model
{
    public partial class DomainContext : DbContext
    {
        public DomainContext()
        {
        }

        public DomainContext(DbContextOptions<DomainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<DummyTable> DummyTables { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductsCompany> ProductsCompanies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=dummy-db;user=root;password=p455w0rd", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");
            });

            modelBuilder.Entity<DummyTable>(entity =>
            {
                entity.ToTable("dummy_table");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
            });

            modelBuilder.Entity<ProductsCompany>(entity =>
            {
                entity.ToTable("products_companies");

                entity.HasIndex(e => e.CompanyId, "IX_products_companies_CompanyId");

                entity.HasIndex(e => e.ProductId, "IX_products_companies_ProductId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ProductsCompanies)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsCompanies)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_products_companies_Products_ProductId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
