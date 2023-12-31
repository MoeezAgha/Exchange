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
        public DbSet<ExchangeOffer> ExchangeOffers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

         


            modelBuilder.Entity<ExchangeOffer>()
                .HasOne(e => e.ExchangeOfferByUser)
                .WithMany(u => u.SendExchangeOffers)
                .HasForeignKey(e => e.ExchangeOfferByUserId)
                .OnDelete(DeleteBehavior.NoAction); // Choose the appropriate cascade action

            modelBuilder.Entity<ExchangeOffer>()
                .HasOne(e => e.Product)
                .WithMany(p => p.ExchangeOffers)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.NoAction); // Choose the appropriate cascade action

        }


        private string GetCurrentUserId()
        {
          
            return "System";
        }
    }


}
