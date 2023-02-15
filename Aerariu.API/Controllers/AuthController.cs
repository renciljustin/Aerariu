using Aerariu.API.Dtos;
using Aerariu.Core;
using Aerariu.Core.Models;
using Aerariu.Utils.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aerariu.API.Controllers
{
    [Route(ApiHelper.ApiRoute)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public AuthController(IUnitOfWork uow, IConfiguration config)
        {
            _uow = uow;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var user = await _uow.UserRepository.GetAsync(user => user.Username == dto.Username);

            if (user != null)
                return BadRequest("Username is already used.");

            var userToCreate = new User
            {
                Username = dto.Username,
                Name = dto.Name,
                Email = dto.Email,
                UserRoles = new List<UserRole>
                {
                    //new UserRole { RoleId = Guid.Parse("a5615233-b9c9-4f37-ba3a-ab4544def138") }, //admin user
                    new UserRole { RoleId = Guid.Parse("9d959dff-deb9-4603-bfe8-6057e5714f4b") } //ordinary user
                }
            };

            await _uow.UserRepository.CreateUserAsync(userToCreate, dto.Password);

            await _uow.CommitAsync();

            var token = await GenerateSecurityToken(userToCreate);
            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(token);

            return CreatedAtRoute("", new
            {
                user = userToCreate.Username,
                token = accessToken
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _uow.UserRepository.GetAsync(user => user.Username == dto.Username);

            if (user is null)
                return Unauthorized("Invalid username.");

            var passwordCheck = await _uow.UserRepository.CheckPasswordAsync(user, dto.Password);

            if (!passwordCheck)
                return Unauthorized("Invalid password.");

            var token = await GenerateSecurityToken(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user = user.Username,
                accessToken
            });
        }

        private async Task<JwtSecurityToken> GenerateSecurityToken(User user)
        {
            var claims = await RenderClaimsAsync(user);
            var credentials = RenderCredentials();

            var securityToken = new JwtSecurityToken(
                _config["Token:Issuer"],
                _config["Token:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: credentials
            );

            return securityToken;
        }

        private async Task<List<Claim>> RenderClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var roles = await _uow.UserRepository.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim("role", role)));

            return claims;
        }

        private SigningCredentials RenderCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Token:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            return credentials;
        }
    }
}
