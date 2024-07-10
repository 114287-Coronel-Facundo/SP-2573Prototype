using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain
{
    internal class DummyContext : DbContext
    {
        public DummyContext(DbContextOptions options) : base(options)
        {
        }
    }
}
