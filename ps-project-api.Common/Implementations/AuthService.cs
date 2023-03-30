using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ps_project_api.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Common.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration config)
        {
            _configuration = config;
        }

        public string GenerateJwtToken(string id, int accountType)
        {
            byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);


            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", id.ToString()),
                    new Claim("accountType", accountType.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string Encrypt(string encryptText)
        {
            SHA256 sha256 = SHA256.Create();
            StringBuilder hashValue = new StringBuilder();
            UTF8Encoding objUtf8 = new UTF8Encoding();

            byte[] crypto = sha256.ComputeHash(objUtf8.GetBytes(encryptText));

            foreach (byte b in crypto)
            {
                hashValue.Append(b.ToString("x2"));
            }

            return hashValue.ToString();
        }

        public string GetSalt(int length)
        {
            // Build the random bytes
            byte[] salt = RandomNumberGenerator.GetBytes(length);

            // Return the string encoded salt
            return Convert.ToBase64String(salt);
        }
    }
}
