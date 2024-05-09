using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentMicroservice.Api.Auth;
using PaymentMicroservice.Api.Models;
using PaymentMicroservice.Shared.Config;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PaymentMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtIssuerOptions _jwtOptions;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<JwtTokenValidationSettings> _jwtConfig;

        public AuthController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IJwtIssuerOptions jwtOptions,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtTokenValidationSettings> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtOptions = jwtOptions;
            _roleManager = roleManager;
            _jwtConfig = jwtConfig;

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var results = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);


            if (user == null || !(await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
            {
                return Unauthorized();
            }

            var tokenString = await CreateJwtTokenAsync(user);

            var result = new ContentResult() { Content = tokenString, ContentType = "application/text" };

            return result;

        }



        private async Task<String> CreateJwtTokenAsync(IdentityUser user)
        {
            var claims = new List<Claim>(new[]
            {
                //Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),

                //Username
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),

                //Email is unique
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                //Unique Id for all Jwt tokens
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),

                //Issued at
                new Claim(JwtRegisteredClaimNames.Iat, _jwtOptions.IssuedAt.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64)
            });

            // Add userclaims from storage
            claims.AddRange(await _userManager.GetClaimsAsync(user));

            //add user role, they are converted to claims
            var roleNames = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                //Find IdentityRole by name
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    //convert identity to claim and add
                    var roleClaim = new Claim(ClaimTypes.Role, role.Name, ClaimValueTypes.String);
                    claims.Add(roleClaim);

                    //Add claims belonging to the role
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            }

            //Prepare Jwt Token
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expires,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            //Serialize token
            var result = new JwtSecurityTokenHandler().WriteToken(jwt);

            return result;
        }
    }
}
