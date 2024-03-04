﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exchange.DAL.Models
{
     [Table("Product", Schema = "Product")]
    public class Product : IAuditable
    {//barter
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string WantDescription { get; set; }

        // Other product properties
        public bool IsPublic { get; set; }
        public bool IsAcceptedOffer { get; set; }
      //  public ExchangeOffer? AcceptedOffer { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        // One product can have many exchange offers
     //   public ICollection<ExchangeOffer>? ExchangeOffers { get; set; }
        public int ProductCreatedByUserId { get; set; } // ForeignKey for ApplicationUser
        public ApplicationUser ProductCreatedByUser { get; set; }

        public string? CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
