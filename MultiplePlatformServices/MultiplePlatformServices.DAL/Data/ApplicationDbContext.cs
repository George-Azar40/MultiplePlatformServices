using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiplePlatformServices.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            builder.Entity<Store>()
                .HasOne(s => s.Vendor)
                .WithMany()
                .HasForeignKey(s => s.VendorId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Product - Store
            // =========================

            builder.Entity<Product>()
                .HasOne(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // Product - Category
            // =========================

            builder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Service - Freelancer
            // =========================

            builder.Entity<Service>()
                .HasOne(s => s.Freelancer)
                .WithMany()
                .HasForeignKey(s => s.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Service - Category
            // =========================

            builder.Entity<Service>()
                .HasOne(s => s.ServiceCategory)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Cart - User
            // =========================

            builder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // Cart - CartItems
            // =========================

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // CartItem - Product
            // =========================

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Order - User
            // =========================

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // Order - OrderItems
            // =========================

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // OrderItem - Product
            // =========================

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // ServiceOrder - Customer
            // =========================

            builder.Entity<ServiceOrder>()
                .HasOne(so => so.Customer)
                .WithMany()
                .HasForeignKey(so => so.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // ServiceOrder - Freelancer
            // =========================

            builder.Entity<ServiceOrder>()
                .HasOne(so => so.Freelancer)
                .WithMany()
                .HasForeignKey(so => so.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // ServiceOrder - Service
            // =========================

            builder.Entity<ServiceOrder>()
                .HasOne(so => so.Service)
                .WithMany()
                .HasForeignKey(so => so.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // Address - User
            // =========================

            builder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
