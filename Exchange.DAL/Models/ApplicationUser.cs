using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.DAL.Models
{
    //public class AuditEntry
    //{
      
    //    public int Id { get; set; }
    //    public string TableName { get; set; }
    //    public string Action { get; set; }
    //    public string UserId { get; set; }
    //    public DateTime Timestamp { get; set; }
    //    public string RecordId { get; set; }
    //    public string PropertyName { get; set; }
    //    public string OriginalValue { get; set; }
    //    public string NewValue { get; set; }
    //}

    public class Audit
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        public string Action { get; set; } // "Create", "Update", "Delete"

        [Required]
        public DateTime ActionDate { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public string RecordId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }

    // [Table("ApplicationUser", Schema = "User")]

    public class ApplicationUser : IdentityUser<int> , IAuditable
    {
        public ICollection<IdentityUserRole<int>> Roles { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ExchangeOffer> ExchangeOffers { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
   // [Table("Product", Schema = "Product")]
    public class Product : IAuditable
    {//barter
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        // Other product properties
        public bool IsPublic { get; set; }
        public bool IsAcceptedOffer { get; set; }
        public ExchangeOffer AcceptedOffer { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<ExchangeOffer> ExchangeOffers { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
   // [Table("ProductImage", Schema = "Product")]
    public class ProductImage 
    {
        public int ProductImageId { get; set; }
        public string ImageUrl { get; set; }
        // Other image properties

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public bool IsPublic { get; set; }

    }
 //   [Table("Category", Schema = "Product")]
    public class Category : IAuditable
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        // Other category properties
        public ICollection<Product> Products { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsPublic { get; set; }
    }
   // [Table("Image", Schema = "Product")]
    public class Image : IAuditable
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        // Other image properties

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public int? TagId { get; set; }
        public Tag Tag { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsPublic { get; set; }
    }
   // [Table("Tag", Schema = "Product")]
    public class Tag : IAuditable
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        // Other tag properties

        public ICollection<Product> Products { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsPublic { get; set; }
    }
  //  [Table("ExchangeOffer", Schema = "Product")]
    public class ExchangeOffer : IAuditable
    {
        public int ExchangeOfferId { get; set; }
        public string OfferName { get; set; }
        // Other offer properties

        public int ProductId { get; set; }
        public Product Product { get; set; }



        public int UserId { get; set; }
        public ApplicationUser ExchangeOfferByUser { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsPublic { get; set; }
    }

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
    .WithMany(u => u.ExchangeOffers)
    .HasForeignKey(e => e.UserId)
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
