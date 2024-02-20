using Exchange.Library.DataTransferObject;
using Exchange.UI.Library.Helper.BaseComponets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Pages.Admin.Categories
{
    public partial class CategoriesComponent : BarterBaseComponet<CategoriesComponent>
    {
        public List<CategoryDTO> _categoryDTO = new(); // Initialize the list

        protected override async Task OnInitializedAsync()
        {

            var response = await _applicationHttpClient.GetJsonAsync<List<CategoryDTO>>("Category?includeProducts=false");

            if (response.statusCode == HttpStatusCode.OK)
            {
                _categoryDTO = response.Data;
            }
            StateHasChanged();
        }
    }
}
