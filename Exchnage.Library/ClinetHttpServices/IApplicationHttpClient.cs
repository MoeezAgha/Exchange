namespace Exchange.Library.ClinetHttpServices
{
    public interface IApplicationHttpClient
    {
        void AddAuthorizationHeader(string jwtToken);
        Task DeleteAsync(string relativeUrl);
        Task<ApiResponse<TResponse>> Get<TResponse>(string relativeUrl);
        Task<ApiResponse<TResponse>> GetJsonAsync<TResponse>(string relativeUrl);
        Task<ApiResponse<TResponse>> PostJsonAsync<TRequest, TResponse>(string relativeUrl, TRequest request);
        Task<TResponse> PutJsonAsync<TRequest, TResponse>(string relativeUrl, TRequest request);

    }
}