using Exchange.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Exchange.Library.DataTransferObject
{
    public class ProductDTO : Product
    {
    }
    public class ProductImageDTO : ProductImage
    {
    }
    [JsonSerializable(typeof(CategoryDTO))]
    public class CategoryDTO 
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product>? Products { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsPublic { get; set; }
    }
    public class ExchangeOfferDTO : ExchangeOffer
    {
    }
    public class ImageDTO : Image
    { 
    }
    public class TagDTO : Tag
    {
    }
}
