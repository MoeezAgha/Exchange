namespace Exchange.Library.DataTransferObject.Account
{
    [JsonSerializable(typeof(TokenResponse))]
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
