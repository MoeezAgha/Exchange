using Exchange.Library.DataTransferObject;

namespace Exchnage.Library.DataTransferObject.Account
{
    [JsonSerializable(typeof(TokenResponse))]
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
