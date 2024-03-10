using Azure;
using Blazored.LocalStorage;
using Exchange.Library.DataTransferObject.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Exchange.UI.Library.Helper.StateProviderHelper
{


    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = null;

            try
            {
                token = await _localStorageService.GetItemAsStringAsync("token");
            }
            catch (InvalidOperationException)
            {
                // Pre-rendering phase, JavaScript interop not available
                // Handle accordingly, possibly by assuming anonymous user state
            }

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }
       
        public async Task NotifyAuthenticationStateChanged()
        {
            var authState = await  GetAuthenticationStateAsync(); // Call your overridden method
              NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
            //if (keyValuePairs.TryGetValue("exp", out object expValue) && expValue is long exp)
            //{
            //    var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(exp);
            //    return dateTimeOffset.UtcDateTime <= DateTime.UtcNow;
            //}
            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }
            }

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
