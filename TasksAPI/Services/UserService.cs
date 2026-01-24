using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;


namespace TasksAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseConnectContext _DBContext;
        private readonly string _pepper;
        private readonly int _iteration = 3;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(DatabaseConnectContext context, IConfiguration configuration, IMapper mapper)
        {
            _DBContext = context ?? throw new ArgumentNullException(nameof(context));
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public async Task<IEnumerable<UserResource>> GetUsers()
        {
            var allUsers = await _DBContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserResource>>(allUsers);
        }

        public async Task<UserResource> GetUserById(int userID)
        {
            var user = await _DBContext.Users.FirstOrDefaultAsync(u => u.Id == userID);
            return _mapper.Map<UserResource>(user);

        }

        public async Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken)
        {
            var checkUser = _DBContext.Users.FirstOrDefault(u => u.Username == resource.Username || u.Email == resource.Email);
            if (checkUser != null)
            {
                throw new ArgumentException("Username or Email already exists.");
            }
            
            var user = new UserEntity
            {
                Username = resource.Username,
                Email = resource.Email,
                FirstName = resource.FirstName,
                LastName = resource.LastName,
                UserTypeId = resource.UserTypeId,
                PasswordSalt = PasswordHasher.GenerateSalt()
            };
            user.PasswordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);

            await _DBContext.Users.AddAsync(user, cancellationToken);
            await _DBContext.SaveChangesAsync(cancellationToken);
            var refreshToken = new RefreshTokenEntity()
            {
                Token = GenerateRefreshToken(),
                Revoked = false,
                userId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddMinutes(15),
                    
            };
            var res =  await saveOrUpdateRefreshToken(refreshToken);
            return _mapper.Map<UserResource>(user);
        }


        
        public async Task<LoginResponse> Login(LoginResource resource, CancellationToken cancellationToken)
        {
            var user = await _DBContext.Users
                .FirstOrDefaultAsync(x => x.Username == resource.Username, cancellationToken) ?? throw new Exception("Username not found.");

            if (user.Status == (int)DBEntityStatus.DISABLED || user.Status == (int)DBEntityStatus.MARK_FOR_DELETE)
                throw new Exception("User disabled.");

            var passwordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);
            if (user.PasswordHash != passwordHash)
               throw new ArgumentException("Username or password did not match.");
            
            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("userType", user.UserTypeId.ToString()),
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName)
            };

            switch (user.UserTypeId) 
            {
                case  3:
                    claimsForToken.Add(new Claim("role", "clerk"));
                    break;
                case  4:
                    claimsForToken.Add(new Claim("role", "clerk"));
                    claimsForToken.Add(new Claim("role", "supervisor"));
                    break;
            }
            var tokenToReturn = GenerateToken(claimsForToken);
            var userprofile =  await GetUserById(user.Id);
            var refreshToken = GenerateRefreshToken();
            return new LoginResponse( tokenToReturn, userprofile,refreshToken) ;
        }

        public Task<string> RefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["Authentification:SecretForkey"];
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(15), // Extend expiration time
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var newToken = tokenHandler.CreateToken(tokenDescriptor);
            return  Task.FromResult(tokenHandler.WriteToken(newToken));
        }

        public async Task<UserResource> UpdateUser(int userID, UserResourceForUpdate userResource, CancellationToken cancellationToken)
        {
            var userToBeUpdated = await _DBContext.Users
            .FirstOrDefaultAsync(x => x.Id == userID, cancellationToken);

            if (userToBeUpdated == null)
            {
               throw new ArgumentException(nameof(userToBeUpdated));
            }

            _mapper.Map(userResource, userToBeUpdated);
            await _DBContext.SaveChangesAsync(cancellationToken);
            var updatedUser = await GetUserById(userToBeUpdated.Id);

            return _mapper.Map<UserResource>(updatedUser);

        }



        public async Task<bool> DeleteUser(int userID, CancellationToken cancellationToken)
        {

            var user = await _DBContext.Users.FirstOrDefaultAsync(x => x.Id == userID, cancellationToken) ?? throw new ArgumentException($"User {userID} not found");

            if (user.Status == (int)DBEntityStatus.MARK_FOR_DELETE && user.UserTypeId == (int)EnumTypes.CLIENT)
            {
                _DBContext.Remove(user);
                await _DBContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                throw new ArgumentException($"User type {user.UserTypeId}, status {user.Status}. Please mark for delete first");
            }
            
        }

        public bool ValidateToken(string authToken)
        {
            var conf = new string[] { _configuration["Authentification:Issuer"], _configuration["Authentification:Audience"], _configuration["Authentification:SecretForkey"] };
            return PasswordHasher.ValidateToken(authToken, conf);
        }

        public async Task<UserResource> PatchUser(int userID, JsonPatchDocument patchUser, CancellationToken cancellationToken)
        {
            var userToPatch = await _DBContext.Users.FirstOrDefaultAsync(u => u.Id == userID, cancellationToken);
            if (userToPatch == null)
            {
               throw new ArgumentException("User not found");
            }
            patchUser.ApplyTo(userToPatch);
            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<UserResource>(userToPatch);
        }
        
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token, SymmetricSecurityKey securityKey)
        {
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = false // We want to get claims from expired token
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private string GenerateToken(List<Claim> claimsForToken)
        {
            var key = _configuration["Authentification:SecretForkey"];
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentification:Issuer"],
                _configuration["Authentification:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private async Task<RefreshToken> saveOrUpdateRefreshToken(RefreshTokenEntity refreshToken)
        {   var tokenToBeUpdated = await _DBContext.RefreshTokenEntity
                .FirstOrDefaultAsync(x => x.Id == refreshToken.userId);
            if (tokenToBeUpdated == null)
            {
                await _DBContext.RefreshTokenEntity.AddAsync(refreshToken);
            }
            else
            {
                _mapper.Map(refreshToken, tokenToBeUpdated);
            }
            await _DBContext.SaveChangesAsync(true);
            return _mapper.Map<RefreshToken>(refreshToken);
        }
    }
}
