using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exchange.DAL.Models
{
    // [Table("Tag", Schema = "Product")]
    public class Tag : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TagId { get; set; }
        [StringLength(30)]

        public string TagName { get; set; }
        // Other tag properties

        public ICollection<Product>? Products { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsPublic { get; set; }
    }
}
