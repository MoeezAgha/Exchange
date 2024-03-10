using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject;
using Exchange.UI.Library.Helper.BaseComponets;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using MudBlazor;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Pages.Admin.Categories
{
    public partial class CategoryCreate : BarterBaseComponent<CategoryCreate>
    {

        [Parameter]
        public string categoryId { get; set; }

        bool success;

        CategoryDTO model = new CategoryDTO();
  


        protected override async Task OnInitializedAsync()
        {
            if (categoryId is not null)
            {
                var tagResponse = await ApplicationHttpClient.GetJsonAsync<CategoryDTO>($"Category/{categoryId}");
                model = tagResponse.Data;
            }
        }
     

    

        void GoBack()
        {
            _navigationManager.NavigateTo("categories");
        }


        async Task OnSubmit(CategoryDTO model)
        {
            try
            {
                var response = await ApplicationHttpClient.PostJsonAsync<CategoryDTO, CategoryDTO>("Category", model);

                if (response.Success)
                {
                    //AlertStyleType = AlertStyle.Success;
                  //  AlertMessage = $"Tag Successfully created {response.Data.TagName}";
                }
                else
                {
               //     AlertStyleType = AlertStyle.Danger;
                   // AlertMessage = $"Failed to create tag: {response.Data.TagName}";

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, nameof(this.GetType));


            }

            StateHasChanged();
        }

        void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
            // console.Log($"InvalidSubmit: {JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true })}");
        }
    }
}
