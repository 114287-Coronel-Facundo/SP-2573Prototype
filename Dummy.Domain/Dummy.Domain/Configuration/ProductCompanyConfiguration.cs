using Dummy.Domain.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain.Configuration
{
    internal class ProductCompanyConfiguration : IEntityTypeConfiguration<ProductCompanies>
    {
        public void Configure(EntityTypeBuilder<ProductCompanies> builder)
        {
            builder.ToTable("products_companies");
            builder.HasKey(p => p.Id);

            builder.HasOne(pc => pc.Products)
                    .WithMany(p => p.ProductsCompanies)
                    .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(pc => pc.Companies)
                .WithMany(c => c.ProductsCompanies)
                .HasForeignKey(pc => pc.CompanyId);
        }
    }
}
