using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Detego.WebAPI.Data.Repositories.Interfaces;
using Detego.WebAPI.Dto;
using Detego.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Detego.WebAPI.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]UserForRegisterDto user)
        {

            var isUserExists = await _authRepository.IsUserExists(user.UserName);
            if (isUserExists)
            {
                ModelState.AddModelError("UserName", "This User Name Is Being Used");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray());
            }

            var newUser = await this._authRepository.Register(user);
            return Ok(newUser);
            
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UserForLoginDto userForRegisterDto)
        {
            var user = await _authRepository.Login(userForRegisterDto.UserName,userForRegisterDto.Password);

            if(user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                    , SecurityAlgorithms.HmacSha512Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);

        }
    }
}