using Dummy.Domain.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain.Configuration
{
    internal class DummyConfiguration : IEntityTypeConfiguration<DummyClass>
    {
        public void Configure(EntityTypeBuilder<DummyClass> builder)
        {
            
            builder.ToTable("Dummy_Table");

            builder.HasKey(p => p.Id);
        }
    }
}
