using Exchange.DAL.Services;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchange.DAL.Models
{
    [Table("NavMenu", Schema = "Menu")]

    public class NavMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        [Required]
        [Display(Name = "Publish this menu item?")]
        public bool IsPublished { get; set; } = false;

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Route { get; set; }

        public string Icon { get; set; }

        public int? ParentMenuId { get; set; }
        [ForeignKey("ParentMenuId")]
        public NavMenu ParentMenu { get; set; }
        public ICollection<NavMenu> SubMenus { get; set; }

        [ForeignKey("Id")]
        public IdentityRole<int> AccessRoleId { get; set; }
     
    }

}
