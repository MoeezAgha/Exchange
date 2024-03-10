using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Exchange.Library.DataTransferObject.Account
{
    [JsonSerializable(typeof(LoginViewModel))]
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
