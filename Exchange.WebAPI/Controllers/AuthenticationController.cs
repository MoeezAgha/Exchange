using Exchange.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exchange.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public JwtSetting _jwtSetting;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //  private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IOptions<JwtSetting> jwtSetting)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtSetting = jwtSetting.Value;
            //_roleManager = roleManager;
        }

        /// <summary>
        /// Application health check 
        /// </summary>
        /// <returns></returns>
        /// []
        /// 
        [Authorize]
        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Api-running");
        }
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="createUserDTO">The data transfer object containing the user details.</param>
        /// <returns>An IActionResult representing the result of the user creation operation.</returns>
        /// 
        [HttpPost]
        [Route("api/users")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            var user = new ApplicationUser { UserName = createUserDTO.Username };

            var result = await _userManager.CreateAsync(user, createUserDTO.Password);
            if (result.Succeeded)
            {
                return Ok("User created successfully.");
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Retrieves user details by user ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An IActionResult representing the result of the user retrieval operation.</returns>
        [HttpGet]
        [Route("api/users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {

                return Ok(user);
            }

            return NotFound();
        }


        //[HttpPost("login")]
        //public IActionResult Login()
        //{
        //    string token =  GenerateJwtToken("agha","2");
        //    return Ok(new { Token = token });
        //}
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user2 = new ApplicationUser { UserName = "yahooo" };
                //var password = "1234567890AsD@";

                //var result = await _userManager.CreateAsync(user2, password);
                //if (result.Succeeded)
                //{
                //    // User created successfully
                //}

                var users = await _userManager.Users.ToListAsync();

                var z = users;
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var r = await GenerateJwtToken(user);
                    return Ok(new { Result = r });
                }

                return Unauthorized();
            }

            return BadRequest(ModelState);
        }
        /// <summary>
        /// Updates user details.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updateUserDTO">The data transfer object containing the updated user details.</param>
        /// <returns>An IActionResult representing the result of the user update operation.</returns>
        [HttpPut]
        [Route("api/users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.UserName = updateUserDTO.Username;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok("User updated successfully.");
                }

                return BadRequest(result.Errors);
            }

            return NotFound();
        }
        /// <summary>
        /// Deletes a user by user ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An IActionResult representing the result of the user deletion operation.</returns>
        [HttpDelete]
        [Route("api/users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok("User deleted successfully.");
                }

                return BadRequest(result.Errors);
            }

            return NotFound();
        }

        /// <summary>
        /// Generates a JWT (JSON Web Token) for an ApplicationUser.
        /// </summary>
        /// <param name="user">The ApplicationUser for which to generate the token.</param>
        /// <returns>A string representing the generated JWT token.</returns>

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                     new Claim(JwtRegisteredClaimNames.Sub,user?.Id.ToString()),
                     new Claim(JwtRegisteredClaimNames.UniqueName, user?.UserName),
                     new Claim(ClaimTypes.Email, user?.Email ?? ""),
                     new Claim(ClaimTypes.Role, "MoeezKhanRole"),
                     new Claim(ClaimTypes.Role, "MoeezKhanRole12"),
                     new Claim(ClaimTypes.Role, "MoeezKhanRole12345"),
              
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.ValidIssuer,
                audience: _jwtSetting.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10), // Set token expiration time
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }

        private bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }

}

public class LoginViewModel
{
    public string UserName { get; set; }

    public string Password { get; set; }
}

public class UpdateUserDTO
{
    public string Username { get; set; }
}
public class JwtSetting
{

    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public string IssuerSigningKey { get; set; }

}
public class CreateUserDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
}
