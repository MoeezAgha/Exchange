using Exchange.Library.DataTransferObject;
using Exchange.UI.Library.Helper.BaseComponets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Pages.Admin.TagArea
{

    public partial class TagComponent : BarterBaseComponet<TagComponent>
    {
        //override OnInitialized

        public List<TagDTO> _tagDTO = new(); // Initialize the list

        protected override async Task OnInitializedAsync()
        {
            var response = await _applicationHttpClient.GetJsonAsync<List<TagDTO>>("Tag?includeProducts=false");

            if (response.statusCode == HttpStatusCode.OK)
            {
                _tagDTO = response.Data;
            }
            StateHasChanged();
        }
    }
}
