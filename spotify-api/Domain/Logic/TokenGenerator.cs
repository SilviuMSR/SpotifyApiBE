using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpotifyApi.Domain.Dtos;
using SpotifyApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic
{
    public class TokenGenerator
    {

        public static string GenerateJwtToken(User user, IMapper mapper, IConfiguration config)
        {

            var mappedUser = mapper.Map<UserDto>(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, mappedUser.Id.ToString()),
                new Claim(ClaimTypes.Name, mappedUser.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

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
