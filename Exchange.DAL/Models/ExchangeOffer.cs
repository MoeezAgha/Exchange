using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exchange.DAL.Models
{
    //  [Table("ExchangeOffer", Schema = "Product")]
    public class ExchangeOffer : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ExchangeOfferId { get; set; }
    
        public string OfferDetails { get; set; }
        // ForeignKey to Product
        public int ProductId { get; set; }
        public Product Product { get; set; }
        // ForeignKey for ApplicationUser who made the offer
        public int ExchangeOfferByUserId { get; set; }
        public ApplicationUser ExchangeOfferByUser { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsPublic { get; set; }
    }
}
