using System;
using EcoSpider.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoSpider.Grpc.Data
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category("Eletronicos") {Id = 1},
                new Category("Roupas") {Id = 2},
                new Category("Sapatos") {Id = 3}
            );
        }
    }
}