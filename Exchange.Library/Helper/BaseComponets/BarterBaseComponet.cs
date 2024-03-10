using Blazored.LocalStorage;
using Exchange.UI.Library.Helper.NavigationMenu;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Components.Authorization;
using AutoMapper;
using Radzen;
using Exchange.Library.ClinetHttpServices;
using Exchange.UI.Library.Pages.Admin.TagArea;
using Microsoft.Extensions.Logging;
using Exchange.UI.Library.Helper.StateProviderHelper;

namespace Exchange.UI.Library.Helper.BaseComponets
{
    public class BarterBaseComponent<T> : ComponentBase, IDisposable
    {
        public  Variant variant = Variant.Outlined;

        [Inject]
        public ApplicationHttpClient? ApplicationHttpClient  { get; set; } = default;

        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        [Inject]
        public IMapper _mapper { get; set; }

        [Inject]
    public    ILogger<TagForm> _logger { get; set; }

        [Inject]
        public CustomAuthenticationStateProvider _customAuthenticationStateProvider { get; set; }
        [Inject]
         public NavigationManager _navigationManager { get; set; }

        public void Dispose()
        {

        }
    }
}

