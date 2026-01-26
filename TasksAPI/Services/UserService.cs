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
            var createdUser = _mapper.Map<UserResource>(user);
            var refreshToken = new RefreshTokenEntity()
            {
                Token = GenerateRefreshToken(),
                Revoked = false,
                userId = createdUser.Id,
                ExpiryDate = DateTime.Now.AddMinutes(15),
                    
            };
            await _DBContext.RefreshTokenEntity.AddAsync(refreshToken);
            await _DBContext.SaveChangesAsync(cancellationToken);
            return createdUser;
        }


        
        public async Task<LoginResponse> Login(LoginResource resource, CancellationToken cancellationToken)
        {
            var user = await _DBContext.Users
                .FirstOrDefaultAsync(x => x.Username == resource.Username, cancellationToken) ?? throw new Exception("Username not found.");

            if (user.Status == (int)DbEntityStatus.DISABLED || user.Status == (int)DbEntityStatus.MARK_FOR_DELETE)
                throw new Exception("User disabled.");

            var passwordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);
            if (user.PasswordHash != passwordHash)
               throw new ArgumentException("Username or password did not match.");
            
            var claimsForToken = GenerateClaimsForToken(user);
            var tokenToReturn = GenerateToken(claimsForToken);
            var userprofile =  await GetUserById(user.Id);
            var refreshToken = new RefreshTokenForUpdate()
            {
                Token = GenerateRefreshToken(),
                Revoked = false,
                UserId = user.Id,
                ExpiryDate = DateTime.Now.AddMinutes(15),
                    
            };
            await UpdateRefreshToken(refreshToken);
            return new LoginResponse( tokenToReturn, userprofile,refreshToken.Token) ;

         
        }

        public Task<string> RefreshToken(RefreshResource resource, CancellationToken cancellationToken)
        {
            var checkUser = _DBContext.Users.FirstOrDefault(u => u.Id == resource.userId);
            var isValidToken = ValidateToken(resource.oldToken, false);
            if (checkUser!=null && isValidToken)
            {
                var claims = GenerateClaimsForToken(checkUser);
                return  Task.FromResult(GenerateToken(claims));
            }
            return Task.FromResult<string>(null);
        }

        public async Task<RefreshToken> GetRefreshToken(RefreshResource resource, CancellationToken cancellationToken)
        {
            var refreshToken = await _DBContext.RefreshTokenEntity.FirstOrDefaultAsync( r => r.Token == resource.refreshToken && r.userId == resource.userId ,cancellationToken);
            return  _mapper.Map<RefreshToken>(refreshToken);;
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

            if (user.Status == (int)DbEntityStatus.MARK_FOR_DELETE && user.UserTypeId == (int)EnumTypes.CLIENT)
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

        public bool ValidateToken(string authToken,bool ValidateLifetime=true)
        {
            var conf = new string[] { _configuration["Authentification:Issuer"], _configuration["Authentification:Audience"], _configuration["Authentification:SecretForkey"] };
            var reply= PasswordHasher.ValidateToken(authToken, conf,ValidateLifetime);
            return reply.IsValid;
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
        

        private string GenerateToken(IEnumerable<Claim> claimsForToken)
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

        private async Task<RefreshToken> UpdateRefreshToken(RefreshTokenForUpdate refreshToken)
        {   var tokenToBeUpdated = await _DBContext.RefreshTokenEntity
                .FirstOrDefaultAsync(x => x.Id == refreshToken.UserId);
            if (tokenToBeUpdated == null)
            {
                throw new ArgumentException(nameof(refreshToken));
            }
            _mapper.Map(refreshToken, tokenToBeUpdated);
            await _DBContext.SaveChangesAsync(true);
            return _mapper.Map<RefreshToken>(await _DBContext.RefreshTokenEntity.FirstOrDefaultAsync(r => r.Token == tokenToBeUpdated.Token));
        }
        
        List<Claim> GenerateClaimsForToken(UserEntity userEntity)
        {
            var claims = new List<Claim>
            {
                new Claim("sub", userEntity.Id.ToString()),
                new Claim("userType", userEntity.UserTypeId.ToString()),
                new Claim("given_name", userEntity.FirstName),
                new Claim("family_name", userEntity.LastName)
            };

            switch (userEntity.UserTypeId) 
            {
                case  3:
                    claims.Add(new Claim("role", "clerk"));
                    break;
                case  4:
                    claims.Add(new Claim("role", "clerk"));
                    claims.Add(new Claim("role", "supervisor"));
                    break;
            }

            return claims;
        }
    }
}
