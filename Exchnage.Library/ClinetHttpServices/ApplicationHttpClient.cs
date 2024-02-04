namespace Exchnage.Library.ClinetHttpServices
{
    public class ApplicationHttpClient(HttpClient httpClient) : IApplicationHttpClient
    {
        private readonly HttpClient httpClient = httpClient;

        public async Task<ApiResponse<T>> GetJsonAsync<T>(string relativeUrl)
        {
            try
            {
                var response = await httpClient.GetAsync(relativeUrl);
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
            var response = await httpClient.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<T>();
            return new ApiResponse<T> { Success = true, Data = data };
        }

        public async Task<ApiResponse<T>> PostJsonAsync<TRequest, T>(string relativeUrl, TRequest request)
        {
            try
            {
                using (var response = await httpClient.PostAsJsonAsync(relativeUrl, request))
                {
                    // var z =response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();
                    //  var z = await response.Content.ReadFromJsonAsync<T>();

                    return new ApiResponse<T> { Success = true, Data = await response.Content.ReadFromJsonAsync<T>(), statusCode = response.StatusCode };
                }

                
           
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Success = false, Message = ex.Message };
            }
        }

        public async Task<T> PutJsonAsync<TRequest, T>(string relativeUrl, TRequest request)
        {
            var response = await httpClient.PutAsJsonAsync(relativeUrl, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task DeleteAsync(string relativeUrl)
        {
            var response = await httpClient.DeleteAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Get it 
        /// </summary>
        /// <param name="jwtToken"></param>

        public void AddAuthorizationHeader(string jwtToken)
        {
            // Add the token to the request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
    }
}
