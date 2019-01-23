using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using SpotifyApi.Domain.Dtos;

namespace SpotifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;


        public UserController(IConfiguration config,
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var usr = await _userManager.FindByNameAsync(user.UserName);

            if(usr == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(usr, user.Password, false);

            if(result.Succeeded)
            {

                var mappedUserToDto = _mapper.Map<UserDto>(user);

                return Ok(new
                {
                    token = GenerateJwtToken(usr)
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            
            var result = await _userManager.CreateAsync(user, user.Password);
            
            if(result.Succeeded)
            {
                return StatusCode(201);
            }

            return BadRequest(result.Errors);
        }

        private string GenerateJwtToken(User user)
        {

            var mappedUser = _mapper.Map<UserDto>(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, mappedUser.Id.ToString()),
                new Claim(ClaimTypes.Name, mappedUser.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);

        }

    }
}
