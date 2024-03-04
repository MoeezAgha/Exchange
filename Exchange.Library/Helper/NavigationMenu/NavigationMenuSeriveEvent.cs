using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Helper.NavigationMenu
{
    public class NavigationMenuSeriveEvent
    {

        public event Action RefreshTheNavMenuEvent;

        public async void RefreshNavMenuChanges() {

            RefreshTheNavMenuEvent?.Invoke();
        }

    }
}
