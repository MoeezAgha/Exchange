using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Exchnage.Library.ClinetHttpServices
{
    public class ApplicationHttpClient
    {
        private readonly HttpClient httpClient;

        public ApplicationHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TResponse> GetJsonAsync<TResponse>(string relativeUrl)
        {
            var response = await httpClient.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<string> Get<TResponse>(string relativeUrl)
        {
            var response = await httpClient.GetAsync(relativeUrl);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public void AddAuthorizationHeader(string jwtToken)
        {
            // Get the JWT token from your authentication flow
            // Obtain the JWT token from your authentication flow

            // Add the token to the request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
        public async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string relativeUrl, TRequest request)
        {
            var response = await httpClient.PostAsJsonAsync(relativeUrl, request);
            // var z =response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            //  var z = await response.Content.ReadFromJsonAsync<TResponse>();
            return await response.Content.ReadFromJsonAsync<TResponse>();
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
    }
}
