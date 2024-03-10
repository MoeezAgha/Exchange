using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Services
{
    public class LoadingService
    {
        public bool IsLoading { get; private set; }

        public event Action OnLoadingStateChanged;

        public void StartLoading() => UpdateLoadingState(true);

        public void StopLoading() => UpdateLoadingState(false);

        private void UpdateLoadingState(bool isLoading)
        {
            IsLoading = isLoading;
            OnLoadingStateChanged?.Invoke();
        }
    }



}
