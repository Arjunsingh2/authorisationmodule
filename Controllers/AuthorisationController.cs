using IdentityServer4.Services;
using IPAuthorisation.Models;
using IPAuthorisation.Provider;
using IPAuthorisation.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAuthorisation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorisationController : ControllerBase
    {
        private readonly UserRepo repo;
        private readonly IConfiguration _config;
      //  private readonly ITokenService _tokenService;
     //   private readonly IUserRepository _userRepository;
        public AuthorisationController(IConfiguration config,UserContext _ctx)
        {
            repo = new UserRepo(_ctx);
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            User user = repo.GetUser(login);
            if (user!=null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
         
        
        [HttpGet]
        [Authorize]
        public  IActionResult authorize()
        {
            return Ok();
        }

        
    }
}
