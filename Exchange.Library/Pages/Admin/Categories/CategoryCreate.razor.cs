using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject;
using Exchange.UI.Library.Helper.BaseComponets;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Pages.Admin.Categories
{
    public partial class CategoryCreate : BarterBaseComponet<CategoryCreate>
    {
       
     
       bool success;
        
        CategoryDTO model = new CategoryDTO();
        private MudForm form;

        public async Task HandleSubmit()
        {
            var a = form.Model as Category;
            //  var z = await _applicationHttpClient.PostJsonAsync<Category>("Category", form.Model as Category);
            var z = await ApplicationHttpClient.PostJsonAsync<Category, Category>("Category", form.Model as Category);


        }
        private void OnValidSubmit(EditContext context)
        {
            var a = form;
            success = true;
            StateHasChanged();
        }
    }
}
