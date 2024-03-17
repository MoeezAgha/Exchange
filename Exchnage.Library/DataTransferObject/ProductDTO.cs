using System.ComponentModel.DataAnnotations;

namespace Exchange.Library.DataTransferObject
{
    [JsonSerializable(typeof(ProductDTO))]
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }

        public string WantDescription { get; set; }

        // Other product properties
        public bool IsPublic { get; set; } = false;
        public int? AcceptedOfferId { get; set; }
        public int? CategoryId { get; set; }
        public ICollection<Tag>? Tags { get; set; } = new HashSet<Tag>();
        public ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
        public int ProductCreatedByUserId { get; set; } // ForeignKey for ApplicationUser
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public string ProductUrl
        {
            get
            {
                // Convert ProductName to a URL-friendly format
                return ProductName?.ToLower().Replace(" ", "-").Replace(".", "");
            }
        }
    }
}
