using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Core.Application.WebApi
{
    public static class TokenUtils
    {
        public static SecurityKey GetKey()
        {
            RSAParameters keyParams;
            using (var rsa = RSA.Create())
            {
                try
                {
                    keyParams = rsa.ExportParameters(true);
                }
                finally
                {
                }
            }

            var key = new RsaSecurityKey(keyParams);

            return key;
        }

        public static SigningCredentials GetCredentials(SecurityKey key)
        {
            return new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}