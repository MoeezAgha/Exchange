using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.DAL.Models
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; } 
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }

    // [Table("ApplicationUser", Schema = "User")]

    public class ApplicationUser : IdentityUser<int> , IAuditable
    {

        //One User can place multiple Products 
        public ICollection<Product> Products { get; set; }

        //One User can have multiple roles default with be user
        public ICollection<IdentityUserRole<int>> Roles { get; set; }


        //One User can send mutiple ExchangeOffer to other user products 
        public ICollection<ExchangeOffer> SendExchangeOffers { get; set; }

        public string? CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }= "System";
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }

   
}
