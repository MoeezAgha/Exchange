

using Exchange.Library.DataTransferObject;
using Exchange.Library.Helper;
using Exchnage.Library.ClinetHttpServices;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Exchange.WebApp.Components.Pages
{
    public partial class Counter : ComponentBase
    {
        //[Inject]
        //public IHttpClientFactory ClientFactory { get; set; }


        [Inject]

        ApplicationHttpClient? HttpClient { get; set; } = default;

        protected override void OnInitialized()
        {
           // _api1Client = ClientFactory.CreateClient(ApiName.ApplicationAPI);
        }

        public List<CategoryDTO> _categories = new List<CategoryDTO>(); // Initialize the list

        protected override async Task OnInitializedAsync()
        {
            var response = await HttpClient.GetJsonAsync<List<CategoryDTO>>("Category");

            if (response.statusCode == HttpStatusCode.OK)
            {
                _categories = response.Data;


            }
            this.StateHasChanged();
        
        }
    }
}
