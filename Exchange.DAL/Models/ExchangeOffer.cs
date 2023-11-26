namespace Exchange.DAL.Models
{
    //  [Table("ExchangeOffer", Schema = "Product")]
    public class ExchangeOffer : IAuditable
    {
        public int ExchangeOfferId { get; set; }
        public string OfferName { get; set; }
        // Other offer properties

        public int ProductId { get; set; }
        public Product Product { get; set; }



        public int UserId { get; set; }
        public ApplicationUser ExchangeOfferByUser { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsPublic { get; set; }
    }
}
