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
        protected ApplicationHttpClient? _applicationHttpClient { get; set; } = default;

        //public void Dispose()
        //{
        //    // will use event to Dispose
        //}
    }
}
