using Exchange.BAL.Services.JWTConfiguration;
using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using NuGet.Packaging;
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
            var user = new ApplicationUser { UserName = createUserDTO.Username.Split('@').FirstOrDefault(), Email = createUserDTO.Username };

            var result = await _userManager.CreateAsync(user, createUserDTO.Password);
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
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
                var user = await _userManager.FindByEmailAsync(model.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    // Get claims
                    var claims = await _userManager.GetClaimsAsync(user);

                    // Get roles for the user
                    var roles = await _userManager.GetRolesAsync(user);


                    var writeToken = await GenerateJwtToken(user, roles,claims);
                    return Ok(new { Token = writeToken });
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

        private async Task<string> GenerateJwtToken(ApplicationUser user, IList<string> roles, IList<Claim> claims)
        {
            try
            {

          
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var UserClaims = new List<Claim>
                {
                     new Claim(JwtRegisteredClaimNames.Sub,user?.Id.ToString()),
                     new Claim(JwtRegisteredClaimNames.UniqueName, user?.UserName),
                     new Claim(ClaimTypes.Email, user?.Email ?? "")

            };
            UserClaims.AddRange(claims);
            // Add role claims
            foreach (var role in roles)
            {

                    UserClaims.Add(new Claim(ClaimTypes.Role, role));
            }

               // roles.ForEach(role => UserClaims.Add(new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.ValidIssuer,
                audience: _jwtSetting.ValidAudience,
                claims: UserClaims,
                expires: DateTime.UtcNow.AddYears(1000), // Set token expiration time
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw;
            }
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
