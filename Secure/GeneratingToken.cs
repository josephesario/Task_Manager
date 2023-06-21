using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Task_Manager.Models;

namespace Task_Manager.Secure
{
    public class GeneratingToken
    {
        public string valideToken { get; }
        public string secrt { get; }

        public GeneratingToken(string role)
        {
            var securityKey = GenerateSecurityKey();
            secrt = Convert.ToBase64String(securityKey);

            valideToken = GenerateToken(role, securityKey);
        }

        private byte[] GenerateSecurityKey()
        {
            var keyBytes = new byte[32]; // 256 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            return keyBytes;
        }

        private string GenerateToken(string role, byte[] securityKey)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, role)
                // Add any other claims you need for the user
            };

            var key = new SymmetricSecurityKey(securityKey);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(60), // Set the token expiration time
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
