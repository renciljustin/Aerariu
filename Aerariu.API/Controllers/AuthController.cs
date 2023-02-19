﻿using Aerariu.API.Dtos;
using Aerariu.Core;
using Aerariu.Core.Models;
using Aerariu.Utils.Constants;
using Aerariu.Utils.Helpers;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(IUnitOfWork uow, IMapper mapper, IConfiguration config)
        {
            _uow = uow;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            // roleid for user: 9d959dff-deb9-4603-bfe8-6057e5714f4b
            var user = await _uow.UserRepository.GetAsync(user => user.Username == dto.Username);

            if (user != null)
                return BadRequest(ErrorMessage.User_DuplicateUsername);

            var userToCreate = _mapper.Map<User>(dto);

            await _uow.UserRepository.CreateUserAsync(userToCreate, dto.Password);

            await _uow.CommitAsync();

            // FOR TESTING ONLY: Returns the user object.
            //return Ok(new
            //{
            //    message = ResponseMessage.RegistrationSuccess,
            //    data = userToCreate
            //});

            return Ok(ResponseMessage.RegistrationSuccess);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _uow.UserRepository.GetAsync(user => user.Username == dto.Username);

            if (user is null)
                return Unauthorized(ErrorMessage.User_InvalidCredentials);

            var passwordCheck = await _uow.UserRepository.CheckPasswordAsync(user, dto.Password);

            if (!passwordCheck)
                return Unauthorized(ErrorMessage.User_InvalidCredentials);

            var accessToken = await GenerateAccessTokenAsync(user);
            var refreshToken = await GenerateRefreshTokenAsync(user.Id);

            await _uow.CommitAsync();

            return Ok(new
            {
                user = user.Username,
                accessToken,
                refreshToken = refreshToken.Token,
            });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromForm] string refreshToken)
        {
            var refreshTokenDb = await _uow.RefreshTokenRepository.GetAsync(rt => rt.Token == refreshToken);

            if (refreshTokenDb is null)
                return UnprocessableEntity(ErrorMessage.RefreshToken_Invalid);

            _uow.RefreshTokenRepository.Revoke(refreshTokenDb);
            await _uow.CommitAsync();

            return Ok();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromForm] string refreshToken)
        {
            var refreshTokenDb = await _uow.RefreshTokenRepository.GetAsync(rt => rt.Token == refreshToken && !rt.IsRevoked);

            if (refreshTokenDb is null)
                return Unauthorized(ErrorMessage.RefreshToken_Invalid);

            if (refreshTokenDb.ValidTo <= DateTime.UtcNow)
                return Unauthorized(ErrorMessage.RefreshToken_Expired);

            var userDb = await _uow.UserRepository.GetAsync(user => user.Id == refreshTokenDb.UserId);

            if (userDb is null)
                return UnprocessableEntity(ErrorMessage.User_NotFound);

            _uow.RefreshTokenRepository.Refresh(refreshTokenDb);

            var accessToken = await GenerateAccessTokenAsync(userDb);

            await _uow.CommitAsync();

            return Ok(new
            {
                accessToken,
                refreshToken,
            });
        }

        private async Task<string> GenerateAccessTokenAsync(User user)
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
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

        private async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
        {
            var refreshToken = await _uow.RefreshTokenRepository.CreateTokenAsync(userId);
            return refreshToken;
        }
    }
}
