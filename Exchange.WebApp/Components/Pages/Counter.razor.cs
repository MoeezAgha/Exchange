﻿using static System.Net.WebRequestMethods;

namespace Exchange.WebApp.Components.Pages
{
    public partial class Counter : BarterBaseComponet<Counter> 
    {
        //[Inject]
        //public IHttpClientFactory ClientFactory { get; set; }

    

        public List<CategoryDTO> _categories = new List<CategoryDTO>(); // Initialize the list

        protected override async Task OnInitializedAsync()
        {
            var response = await _applicationHttpClient.GetJsonAsync<List<CategoryDTO>>("Category");

            if (response.statusCode == HttpStatusCode.OK)
            {
                _categories = response.Data;


            }
            this.StateHasChanged();
        
        }
    }
}
