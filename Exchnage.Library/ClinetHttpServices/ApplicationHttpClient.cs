using System.Net.Http.Headers;
using System.Net.Http.Json;
using Exchnage.Library.Helper;


namespace Exchnage.Library.ClinetHttpServices
{
    public class ApplicationHttpClient(HttpClient httpClient) : IApplicationHttpClient
    {
        private readonly HttpClient httpClient = httpClient;

        public async Task<ApiResponse<TResponse>> GetJsonAsync<TResponse>(string relativeUrl)
        {
            try
            {
                var response = await httpClient.GetAsync(relativeUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<TResponse>();
                return new ApiResponse<TResponse> { Success = true, Data = data, statusCode = response.StatusCode };
            }
            catch (Exception ex)
            {
                return new ApiResponse<TResponse> { Success = false, Message = ex.Message };
            }
        }
        public async Task<ApiResponse<TResponse>> Get<TResponse>(string relativeUrl)
        {
            var response = await httpClient.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<TResponse>();
            return new ApiResponse<TResponse> { Success = true, Data = data };
        }



        public async Task<ApiResponse<TResponse>> PostJsonAsync<TRequest, TResponse>(string relativeUrl, TRequest request)
        {
            try
            {

                var response = await httpClient.PostAsJsonAsync(relativeUrl, request);
                // var z =response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                //  var z = await response.Content.ReadFromJsonAsync<TResponse>();

                return new ApiResponse<TResponse> { Success = true, Data = await response.Content.ReadFromJsonAsync<TResponse>(), statusCode = response.StatusCode };
            }
            catch (Exception ex)
            {
                return new ApiResponse<TResponse> { Success = false, Message = ex.Message };
            }
        }

        public async Task<TResponse> PutJsonAsync<TRequest, TResponse>(string relativeUrl, TRequest request)
        {
            var response = await httpClient.PutAsJsonAsync(relativeUrl, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task DeleteAsync(string relativeUrl)
        {
            var response = await httpClient.DeleteAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
        }



        public void AddAuthorizationHeader(string jwtToken)
        {
            // Add the token to the request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
    }
}
