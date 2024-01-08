using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchange.DAL.Models
{
    //   [Table("Category", Schema = "Product")]
    public class Category : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CategoryId { get; set; }
        [StringLength(30)]

        public string CategoryName { get; set; }
        // Other category properties
        public ICollection<Product>? Products { get; set; }
        //public string? ImageUrl { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsPublic { get; set; }
    }
}
