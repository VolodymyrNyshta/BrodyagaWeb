using BrodyagaWeb.Data;
using BrodyagaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BrodyagaWeb.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly BrodyagaWebContext _context;

        public AccountController(IOptions<JwtSettings> jwtSettings, BrodyagaWebContext brodyagaWebContext)
        {
            _jwtSettings = jwtSettings.Value;
            _context = brodyagaWebContext;
        }

        public string GetToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "Soldat"));
            claims.Add(new Claim("level", "123"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var jwt = new JwtSecurityToken(
                                            issuer: _jwtSettings.Issure,
                                            audience: _jwtSettings.Audience,
                                            claims: claims,
                                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
                                            notBefore: DateTime.UtcNow,
                                            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                                          );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        //public User Authenticate(UserLogin userLogin)
        //{
        //    var currentUser = _context.GetUsers().FirstOrDefault(x => x.Id == userLogin.Id &&  x.FirstName.ToLower() == userLogin.UserName.ToLower());
           
        //    if (currentUser != null)
        //    {
        //        return currentUser;
        //    }
        //    else return null;
        //}
    }
}
