namespace Exchange.DAL.Models
{
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

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsPublic { get; set; }
    }
}
