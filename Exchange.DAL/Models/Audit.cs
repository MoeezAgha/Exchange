using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Exchange.DAL.Models
{
      public class Audit
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        public string Action { get; set; } // "Create", "Update", "Delete"

        [Required]
        public DateTime ActionDate { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public string RecordId { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
