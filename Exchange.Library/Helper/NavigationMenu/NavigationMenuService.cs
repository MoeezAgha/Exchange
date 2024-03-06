using Blazored.LocalStorage;
using Exchange.Library.DataTransferObject;

using Exchnage.Library.ClinetHttpServices;
using Exchnage.Library.DataTransferObject.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Helper.NavigationMenu
{
    public class NavigationMenuService
    {
        private readonly ApplicationHttpClient _httpClient;
        private List<NavMenuDTO> _navMenuItems;
        private bool _isLoaded = false;
        private ILocalStorageService LocalStorageService;

        public NavigationMenuService(ApplicationHttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            LocalStorageService = localStorageService;
        }

        public async Task<List<NavMenuDTO>> GetNavMenuItemsAsync()
        {
            try
            {
                if (!_isLoaded)
                    {
                    var token = await LocalStorageService.GetItemAsStringAsync("token");
                    

                    if (token != null)
                    {
                        var response = await _httpClient.GetJsonAsync<List<NavMenuDTO>>("NavMenu");
                        _navMenuItems = response.Data;
                        _isLoaded = true;
                    }
                
                    }
                

             

            }
            catch (Exception e)
            {

                
            }
        
            return _navMenuItems;
        }
    }

}
