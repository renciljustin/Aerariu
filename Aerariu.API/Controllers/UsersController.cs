using Aerariu.API.Dtos;
using Aerariu.API.Lib.Middleware.Response;
using Aerariu.Core;
using Aerariu.Utils.Constants;
using Aerariu.Utils.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace Aerariu.API.Controllers
{
    [Authorize]
    [Route(ApiHelper.ApiRoute)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsersController(IUnitOfWork uow, IMapper mapper, IConfiguration config) {
            _uow = uow;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet("Account")]
        public async Task<IActionResult> GetUserById()
        {
            var token = ValidateAndExtractIdFromJWT();

            if (!token.IsValid)
                return Unauthorized(new GenericResponse
                {
                    Message = ErrorMessage.Token_Invalid,
                    StatusCode = StatusCodes.Status401Unauthorized
                });

            var dbUser = await _uow.UserRepository.GetAsync(u => u.Id == token.UserId);

            if (dbUser == null)
                return Unauthorized(new GenericResponse
                {
                    Message = ErrorMessage.User_NotFound,
                    StatusCode = StatusCodes.Status401Unauthorized
                });

            var resUser = _mapper.Map<UserDetailDto>(dbUser);

            return Ok(new ResponseWithData<UserDetailDto>
            {
                Message = ResponseMessage.Success,
                StatusCode = StatusCodes.Status200OK,
                ResultData = resUser
            });
        }

        private (bool IsValid, Guid UserId) ValidateAndExtractIdFromJWT()
        {
            var claims = HttpContext.User.Claims;

            if (claims == null)
                return (false, Guid.Empty);

            var nameId = claims.FirstOrDefault(c => c.Type.EndsWith("nameidentifier"))?.Value;

            if (nameId == null)
                return (false, Guid.Empty);

            var userId = Guid.Parse(nameId);

            return (true, userId);
        }
    }
}
