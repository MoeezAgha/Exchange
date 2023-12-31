using Exchange.Library.DataTransferObject;
using Exchnage.Library.ClinetHttpServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library
{

    public partial class Component1 : ComponentBase
    {

        [Inject]

        public ApplicationHttpClient? HttpClient { get; set; } = default;


        [Inject]
        public HttpClient _httpClient { get; set; } = default;


        protected override void OnInitialized()
        {
            // _api1Client = ClientFactory.CreateClient(ApiName.ApplicationAPI);
        }

        public List<CategoryDTO> _categories = new List<CategoryDTO>(); // Initialize the list

        protected override async Task OnInitializedAsync()
        {
            try
            {

                var response = await HttpClient.GetJsonAsync<List<CategoryDTO>>("Category");

                if (response.statusCode == HttpStatusCode.OK)
                {
                    _categories = response.Data;


                }
            }
            catch (Exception e)
            {

                throw;
            }
            StateHasChanged();

        }
    }
}
