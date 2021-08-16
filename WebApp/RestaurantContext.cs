using RestaurantApp.Core;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApp.Data
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant")
                .HasMany(r => r.Dishes)
                .WithOne(d => d.Restaurant);

            modelBuilder.Entity<Dish>().ToTable("Dish");
        }
    }
}