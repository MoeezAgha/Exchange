using Azure.Core;
using Blazored.LocalStorage;
using Exchange.Library.DataTransferObject;
using Exchange.Library.DataTransferObject.Account;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json;

namespace Exchange.Library.ClinetHttpServices
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
            _ = GetTokenAsync();
        }

        private async Task<string> GetTokenAsync()
        {
            
            if (token == null)
            {
                // _token =  await _localStorageService.GetItemAsync<TokenResponse>("token");
                token = JsonSerializer.Deserialize<string>(await _localStorageService.GetItemAsStringAsync("token"));

                AddAuthorizationHeader(token);
            }
            return token;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> GetJsonAsync<T>(string relativeUrl)
        {
            try
            {
              
                using (var response = await _httpClient.GetAsync(relativeUrl))
                {

                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadFromJsonAsync<T>();
                    return new ApiResponse<T> { Success = true, Data = data, statusCode = response.StatusCode };
                }


            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Success = false, Message = ex.Message };
            }
        }

    

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> Get<T>(string relativeUrl)
        {
            var response = await _httpClient.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<T>();
            return new ApiResponse<T> { Success = true, Data = data };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="relativeUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> PostJsonAsync<TRequest, T>(string relativeUrl, TRequest request)
        {
            try
            {
                using (var response = await _httpClient.PostAsJsonAsync(relativeUrl, request))
                {
                    response.EnsureSuccessStatusCode();
                    return new ApiResponse<T>
                    {
                        Success = response.IsSuccessStatusCode,
                        Data = await response.Content.ReadFromJsonAsync<T>(),
                        statusCode = response.StatusCode
                    };
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
