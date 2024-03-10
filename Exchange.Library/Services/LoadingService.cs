using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Services
{
    public class LoadingService
    {
        private bool _isLoading = false;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                // Optionally, invoke an event to notify components about the state change
            }
        }

        // Implement methods to set and clear the loading state based on your application's needs
        public void StartLoading()
        {
            IsLoading = true;
        }

        public void StopLoading()
        {
            IsLoading = false;
        }
    }


}
