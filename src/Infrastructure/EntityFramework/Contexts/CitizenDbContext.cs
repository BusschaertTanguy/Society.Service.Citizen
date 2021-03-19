using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Contexts
{
    public class CitizenDbContext : DbContext
    {
        public CitizenDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}