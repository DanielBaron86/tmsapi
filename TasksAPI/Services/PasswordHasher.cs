using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace TasksAPI.Services
{
    public static class PasswordHasher
    {
        public static string ComputeHash(string password, string salt, string pepper, int iteration)
        {
            if (iteration <= 0) return password;

            using var sha256 = SHA256.Create();
            var passwordSaltPepper = $"{password}{salt}{pepper}";
            var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
            var byteHash = sha256.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);
            return ComputeHash(hash, salt, pepper, iteration - 1);
        }

        public static string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            rng.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return salt;
        }

        public static bool ValidateToken(string authToken, string[] conf)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(conf);

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }

        private static TokenValidationParameters GetValidationParameters(string[] conf)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, // Because there is no expiration in the generated token
                ValidateAudience = true, // Because there is no audiance in the generated token
                ValidateIssuer = true,   // Because there is no issuer in the generated token
                ValidIssuer = conf[0],
                ValidAudience = conf[1],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf[2])) // The same key as the one that generate the token
            };
        }
    }
}
