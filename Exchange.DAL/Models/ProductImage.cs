namespace Exchange.DAL.Models
{
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
}
