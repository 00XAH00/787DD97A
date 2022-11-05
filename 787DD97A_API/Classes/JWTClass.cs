using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace _787DD97A_API.Classes
{
    public class JWTClass
    {
        private readonly IConfiguration _configuration;

        public JWTClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenJWT(string username, int role)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:KEY").Value));

            var jwt = new JwtSecurityToken(
                issuer: _configuration.GetSection("AppSettings:ISSUER").Value,
                audience: _configuration.GetSection("AppSettings:AUDIENCE").Value,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(Convert.ToDouble(_configuration.GetSection("AppSettings:LIFETIME").Value))),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}

