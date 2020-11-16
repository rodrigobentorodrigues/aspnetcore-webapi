using ASPNET_Core.Data.Maps;
using ASPNET_Core.Models;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_Core.Data
{
    public class StoreDataContext : DbContext
    {
        public StoreDataContext(DbContextOptions<StoreDataContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.Ignore<Notification>(); 
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
