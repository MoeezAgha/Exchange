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

 

            //// Configure the one-to-many relationship between Product and ExchangeOffer
            //modelBuilder.Entity<Product>()
            //    .HasMany(p => p.ExchangeOffers) // Assuming Product has a collection of ExchangeOffers
            //    .WithOne(e => e.Product) // Assuming ExchangeOffer has a navigation property to Product
            //    .HasForeignKey(e => e.ProductId); // Assuming ExchangeOffer has a foreign key property to Product

        }


        private string GetCurrentUserId()
        {
          
            return "System";
        }
    }


}
