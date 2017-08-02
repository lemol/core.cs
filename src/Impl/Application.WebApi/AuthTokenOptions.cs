using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Sedic.Application.WebApi
{
    public class AuthTokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}