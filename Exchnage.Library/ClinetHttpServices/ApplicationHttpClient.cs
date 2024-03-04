using Blazored.LocalStorage;
using Exchnage.Library.DataTransferObject.Account;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json;

namespace Exchnage.Library.ClinetHttpServices
{
    public class ApplicationHttpClient : IApplicationHttpClient
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorageService;
        private TokenResponse _token;
        private string token;
        public ApplicationHttpClient(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        private async Task<string> GetTokenAsync()
        {
          //  return new TokenResponse { Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1IiwidW5pcXVlX25hbWUiOiJ5YWhvbzIyMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InlhaG9vMjIyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MzMyNjYwMDQ1MzQsImlzcyI6InlvdXItaXNzdWVyIiwiYXVkIjoieW91ci1hdWRpZW5jZSJ9.EKI4i83W30eycZ81VZCzWjRxbIAYmx9EvmpGvdivy3E" };
            if (token == null)
            {
               // _token =  await _localStorageService.GetItemAsync<TokenResponse>("token");
                 token = JsonSerializer.Deserialize<string>(await _localStorageService.GetItemAsStringAsync("token"));
             //   _token = JsonSerializer.Deserialize<TokenResponse>(tokenJson);

             //   token = await _localStorageService.GetItemAsStringAsync("token");
            }
            return token;
        }
        public async Task<ApiResponse<T>> GetJsonAsync<T>(string relativeUrl)
        {
            try
            {

                var token = await  GetTokenAsync();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync(relativeUrl);
            

                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<T>();
                return new ApiResponse<T> { Success = true, Data = data, statusCode = response.StatusCode };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Success = false, Message = ex.Message };
            }
        }
        public async Task<ApiResponse<T>> Get<T>(string relativeUrl)
        {
            var response = await _httpClient.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<T>();
            return new ApiResponse<T> { Success = true, Data = data };
        }

        public async Task<ApiResponse<T>> PostJsonAsync<TRequest, T>(string relativeUrl, TRequest request)
        {
            try
            {
                using (var response = await _httpClient.PostAsJsonAsync(relativeUrl, request))
                {
                    response.EnsureSuccessStatusCode();
                    var apiResponse = await response.Content.ReadFromJsonAsync<T>();

                    return new ApiResponse<T> { Success = true, Data = apiResponse, statusCode = response.StatusCode };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Success = false, Message = ex.Message };
            }
        }

        public async Task<T> PutJsonAsync<TRequest, T>(string relativeUrl, TRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(relativeUrl, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task DeleteAsync(string relativeUrl)
        {
            var response = await _httpClient.DeleteAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Get it 
        /// </summary>
        /// <param name="jwtToken"></param>

        public void AddAuthorizationHeader(string jwtToken)
        {
            // Add the token to the request headers
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
    }
}
