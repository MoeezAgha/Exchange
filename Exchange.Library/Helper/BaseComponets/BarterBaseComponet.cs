using Blazored.LocalStorage;
using Exchange.UI.Library.Helper.NavigationMenu;
using Exchnage.Library.ClinetHttpServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Components.Authorization;
namespace Exchange.UI.Library.Helper.BaseComponets
{
    public class BarterBaseComponet<T> : ComponentBase, IDisposable
    {

        [Inject]
        public ApplicationHttpClient? _applicationHttpClient { get; set; } = default;

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }


        //[Inject]
        //public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        //[Inject]
        //public NavigationMenuService NavigationMenuService { get; set; }

        //   [Inject]
        //    public NavigationManager _navigationManager { get; set; }

        public void Dispose()
        {

        }
    }
}

