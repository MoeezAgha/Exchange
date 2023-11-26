namespace Exchange.DAL.Models
{
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


        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
