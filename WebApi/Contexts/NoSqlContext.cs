using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Contexts
{
    public class NoSqlContext : DbContext
    {
        public NoSqlContext()
        {

        }
        public NoSqlContext(DbContextOptions<NoSqlContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToContainer("Products").HasPartitionKey(x => x.Id);
        }
    }
}
