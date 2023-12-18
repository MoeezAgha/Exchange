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



            //modelBuilder.Entity<Product>()
            //   .HasOne(p => p.User)
            //   .WithMany(u => u.Products)
            //   .HasForeignKey(p => p.UserId)
            //   .IsRequired();

            //modelBuilder.Entity<ExchangeOffer>()
            //    .HasOne(e => e.Product)
            //    .WithMany(p => p.ExchangeOffers)
            //    .HasForeignKey(e => e.ProductId)
            //    .IsRequired();

            //modelBuilder.Entity<ExchangeOffer>()
            //    .HasOne(e => e.ExchangeOfferByUser)
            //    .WithMany(u => u.ExchangeOffers)
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired();
        }

        //public override int SaveChanges()
        //{
        //    var auditEntries = OnBeforeSaveChanges();

        //    var result = base.SaveChanges();

        //    OnAfterSaveChanges(auditEntries);

        //    return result;
        //}

        //private List<AuditEntry> OnBeforeSaveChanges()
        //{
        //    var auditEntries = new List<AuditEntry>();

        //    var timestamp = DateTime.UtcNow;
        //    var userId = GetCurrentUserId(); // Implement a method to get the current user ID

        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        if (entry.Entity is IAuditable auditableEntity)
        //        {
        //            var auditEntry = new AuditEntry
        //            {
        //                TableName = entry.Metadata.GetTableName(),
        //                Action = entry.State.ToString(),
        //                UserId = userId,
        //                Timestamp = timestamp,
        //                RecordId = entry.OriginalValues["Id"].ToString(), // Assuming Id is the primary key
        //                                                                  // Other properties...
        //            };

        //            auditEntries.Add(auditEntry);

        //            if (entry.State == EntityState.Added)
        //            {
        //                auditableEntity.CreatedBy = userId;
        //                auditableEntity.CreatedDate = timestamp;
        //            }
        //            else if (entry.State == EntityState.Modified)
        //            {
        //                auditableEntity.ModifiedBy = userId;
        //                auditableEntity.ModifiedDate = timestamp;
        //            }
        //        }
        //    }

        //    return auditEntries;
        //}


        //private void OnAfterSaveChanges(List<AuditEntry> auditEntries)
        //{
        //    foreach (var auditEntry in auditEntries)
        //    {
        //        var entry = ChangeTracker.Entries()
        //            .FirstOrDefault(e => e.Metadata.GetTableName() == auditEntry.TableName &&
        //                                  e.OriginalValues["Id"].ToString() == auditEntry.RecordId);

        //        if (entry != null)
        //        {
        //            auditEntry.Action = entry.State.ToString();
        //            Audits.Add(auditEntry.ToAudit());
        //        }
        //    }

        //    SaveChanges();
        //}
        //public Audit ToAudit()
        //{
        //    return new Audit
        //    {
        //        TableName = TableName,
        //        Action = Action,
        //        ActionDate = Timestamp,
        //        RecordId = RecordId,
        //        // Set CreatedBy, CreatedDate, ModifiedBy, and ModifiedDate based on your requirements
        //    };
        //}

        // Other methods and properties...

        private string GetCurrentUserId()
        {
            // Implement a method to retrieve the current user's ID from your authentication system
            // This might involve accessing HttpContext.User or another means of authentication
            // For simplicity, you can return a placeholder value here
            return "System";
        }
    }


}
