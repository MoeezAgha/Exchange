using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exchange.DAL.Models
{
    // [Table("ProductImage", Schema = "Product")]
    public class ProductImage 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductImageId { get; set; }
        public string ImageUrl { get; set; }
        // Other image properties

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public bool IsPublic { get; set; }

    }
}
