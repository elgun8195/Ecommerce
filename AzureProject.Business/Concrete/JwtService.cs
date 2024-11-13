using AzureProject.Business.Abstract;
using AzureProject.DataAccess.Abstract;
using AzureProject.Entity.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.Business.Concrete
{
    public class JwtService : IJwtService
    {
        public string Genereate(AppUser user, IList<string> roles, IConfiguration configuration)
        {
            //jwt create
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("FullName",user.FullName),

            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());

            string keyStr = configuration.GetSection("JWT:Secret").Value;

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: creds,
                    expires: DateTime.Now.AddDays(3),
                    issuer: configuration.GetSection("JWT:Issuer").Value,
                    audience: configuration.GetSection("JWT:Audience").Value
                );

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }
    }
}
