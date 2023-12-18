namespace Exchnage.Library.DataTransferObject
{
    public class ProductDTO
    {
        public string ProductName { get; set; }
        public bool IsPublic { get; set; }
        public bool IsAcceptedOffer { get; set; }
        public int ProductCreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

}
