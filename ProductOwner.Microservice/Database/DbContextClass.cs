using Microsoft.EntityFrameworkCore;
using ProductOwner.Microservice.Models;

namespace ProductOwner.Microservice.Database
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Products> Products { get; set; }

        public DbSet<ProductOfferDetail> ProductOfferDetails { get; set; }
    }
}
