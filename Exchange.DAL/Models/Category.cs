namespace Exchange.DAL.Models
{
    //   [Table("Category", Schema = "Product")]
    public class Category : IAuditable
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        // Other category properties
        public ICollection<Product> Products { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsPublic { get; set; }
    }
}
