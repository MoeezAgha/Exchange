using Blazored.LocalStorage;
using Exchnage.Library.ClinetHttpServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Helper.BaseComponets
{
    public class BarterBaseComponet<T> : ComponentBase/*, IDisposable*/
    {

        [Inject]
        protected ApplicationHttpClient? ApplicationHttpClient { get; set; } = default;

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        //public void Dispose()
        //{
        //    // will use event to Dispose
        //}
    }
}
