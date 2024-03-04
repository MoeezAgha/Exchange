using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exchange.Library.DataTransferObject
{
    [JsonSerializable(typeof(CategoryDTO))]
    public class CategoryDTO :Category
    {
 
    }

    [JsonSerializable(typeof(NavMenu))]

    public class NavMenuDTO : NavMenu
    {
     

    }

}
