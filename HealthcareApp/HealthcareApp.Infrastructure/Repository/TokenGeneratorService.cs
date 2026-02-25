using System.IdentityModel.Tokens.Jwt;
using HealthcareApp.Infrastructure.Configuration;
using HealthcareApp.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using HealthcareApp.Application.Abstractions;


namespace HealthcareApp.Infrastructure.Repository
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly JwtOptions _jwtOptions;

        public TokenGeneratorService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public Task<string> GenerateJwtToken(ApplicationUser user, List<string> userRoles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("FirstName", user?.FirstName ?? ""),
                new Claim("LastName", user?.LastName ?? ""),
                new Claim("Email", user?.Email ?? "")

            };

            foreach (var role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.DurationInMinutes),
                signingCredentials: credentials
                );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}