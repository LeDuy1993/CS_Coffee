using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CS_Coffee.Models
{
    public class CoffeeContext : IdentityDbContext<ApplicationUser>
    {
   
        public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options)
        {

        }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductOrderDetail> ProductOrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductOrderDetail>().HasKey(pod => new { pod.OrderDetailId, pod.ProductId });
        
        }
    }
}
