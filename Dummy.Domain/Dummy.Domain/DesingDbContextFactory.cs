using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dummy.Domain
{
    internal class DesingDbContextFactory : IDesignTimeDbContextFactory<DummyContext>
    {
        public DummyContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DummyContext>();
            //builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new DummyContext(builder.Options);
        }
    }
}
