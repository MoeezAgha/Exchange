using Exchange.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Exchange.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Audit> Audits { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NavMenu> NavMenu { get; set; }
        public DbSet<ExchangeOffer> ExchangeOffer { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Barter;Integrated Security=True;Encrypt=False");
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuring the one-to-one relationship between Product and AcceptedOffer
            modelBuilder.Entity<Product>()
                .HasOne(p => p.AcceptedOffer)
                .WithOne()
                .HasForeignKey<ExchangeOffer>(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Restricting cascade delete to prevent cycles

            // Configuring the one-to-many relationship between Product and ExchangeOffers
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ExchangeOffers) // Assuming you have a collection of ExchangeOffers in Product
                .WithOne(e => e.Product) // Assuming there's an inverse navigation property in ExchangeOffer
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Again, restricting cascade delete

        }


        private string GetCurrentUserId()
        {
          
            return "System";
        }
    }


}
