using Exchange.Library.DataTransferObject;
using Exchange.UI.Library.Helper.BaseComponets;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Pages.Admin.TagArea
{
    public partial class TagForm : BarterBaseComponent<TagForm>
    {
        AlertStyle AlertStyleType;
        string AlertMessage = string.Empty;
        TagDTO model = new TagDTO();

        [Parameter]
        public string TagId { get; set; }

 

        protected override async Task OnInitializedAsync()
        {
            var z = GetType().Name;
            if (TagId is not null) {
                var tagResponse = await ApplicationHttpClient.GetJsonAsync<TagDTO>($"tag/{TagId}");
                model =  tagResponse.Data;
            }
          //  return base.OnInitializedAsync();
        }

        async Task OnSubmit(TagDTO model)
        {
            try
            {
                var response = await ApplicationHttpClient .PostJsonAsync<TagDTO, TagDTO>("Tag", model);

                if (response.Success)
                {
                    AlertStyleType = AlertStyle.Success;
                    AlertMessage = $"Tag Successfully created {response.Data.TagName}";
                }
                else
                {
                    AlertStyleType = AlertStyle.Danger;
                    AlertMessage = $"Failed to create tag: {response.Data.TagName}";

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,nameof(this.GetType));


            }

            StateHasChanged();
        }

        void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
            // console.Log($"InvalidSubmit: {JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true })}");
        }
        void GoBack()
        {
            _navigationManager.NavigateTo("tags");
        }


    }
}
