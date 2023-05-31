using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Helpers
{
    public static class JWTHelper
    {
        public static string GenerateJWTToken(string secret, ApplicationUser applicationUser, string address, List<string> roles = null)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var subject = new ClaimsIdentity(new[] {
                    new Claim("id", applicationUser.Id),
                    new Claim("email", applicationUser.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("firstName", applicationUser.FirstName),
                    new Claim("lastName", applicationUser.LastName),
                });

            if (!string.IsNullOrEmpty(address))
            {

                subject.AddClaim(new Claim("address", address));
            }

            if (roles != null)
            {
                roles.ForEach(role =>
                {
                    subject.AddClaim(new Claim("roles", role));
                });
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddYears(99),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}