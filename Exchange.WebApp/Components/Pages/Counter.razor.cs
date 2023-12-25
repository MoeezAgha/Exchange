using Exchange.BAL.Services.Contracts;
using Exchange.BAL.Services.Repositories;
using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject;
using Exchange.Library.Helper;
using Exchnage.Library.ClinetHttpServices;
using Microsoft.AspNetCore.Components;
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

            if (true)
            {
                //try
                //{
                //    var responseContent = await response.Content.ReadAsStringAsync();
                //    response.EnsureSuccessStatusCode();
                //    var zz = JsonSerializer.Deserialize<List<object>>(responseContent); // Assign the deserialized data to the _categories variable
                //}
                //catch (JsonException ex)
                //{
                   
                //}
            
            }
            else
            {
                // Handle the error...
            }
            // Handle the response...
        }
    }
}
