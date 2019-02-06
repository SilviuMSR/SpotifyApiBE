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
using SpotifyApi.Domain.Logic;
using SpotifyApi.Domain;

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
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(userDto.UserName);

            if(user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

            if(result.Succeeded)
            {

                var mappedUserToDto = _mapper.Map<UserToReturnDto>(user);

                return Ok(new
                {
                    token = TokenGenerator.GenerateJwtToken(user, _mapper, _config),
                    user = mappedUserToDto
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            
            var result = await _userManager.CreateAsync(user, userDto.Password);
            
            if(result.Succeeded)
            {
                return StatusCode(201);
            }

            return BadRequest(result.Errors);
        }
    }
}
