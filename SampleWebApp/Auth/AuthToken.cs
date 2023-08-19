using DataLayer.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SampleWebApp.Auth
{
    public class AuthToken
    {
        public static ClaimsPrincipal SetToken(string UserName)
        {
          var   identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
        
    }
}
