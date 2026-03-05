using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Interfaces;
using TasksAPI.Models;

namespace TasksAPI.Services
{
    public class ClientService : IClientServices
    {
        private readonly DatabaseConnectContext _DBContext;
        private readonly string _pepper;
        private readonly int _iteration = 3;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ClientService(DatabaseConnectContext context, IConfiguration configuration, IMapper mapper)
        {
            _DBContext = context ?? throw new ArgumentNullException(nameof(context));
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<UserResource> GetClientById(int userID)
        {
            var user = await _DBContext.Accounts.FirstOrDefaultAsync(u => u.Id == userID);
            return _mapper.Map<UserResource>(user);

        }
        public async Task<(IEnumerable<UserResource>, PaginationMetadata)> GetClients(int pageNumber, int pageSize)
        {
            var collection = _DBContext.Accounts
                as IQueryable<Accounts>;
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);
            var collectionReturn = await collection.OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var returnCollection = _mapper.Map<IEnumerable<UserResource>>(collectionReturn);
            return (returnCollection, paginationMetadata);
        }

        public async Task<(IEnumerable<UserResource>, PaginationMetadata)> GetClientsWithConditions(QueryFilters queryFilters)
        {
            var pageSize = queryFilters.pageSize;
            var pageNumber = queryFilters.pageNumber;
            var collection = _DBContext.Accounts
                as IQueryable<Accounts>;
            if (queryFilters.queryFields != null)
            {
                foreach (var q in queryFilters.queryFields)
                {
                    collection = ServiceUtils.CreateFilter(collection, q.keyField, q.keyValue);
                }
            }
            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, queryFilters.pageSize, queryFilters.pageNumber);
            var collectionReturn = await collection.OrderBy(c => c.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var returnCollection = _mapper.Map<IEnumerable<UserResource>>(collectionReturn);
            return (returnCollection, paginationMetadata);
        }

        public async Task<UserResource> Register(ClientResource resource, CancellationToken cancellationToken)
        {
            var checkUser = await _DBContext.Accounts.FirstOrDefaultAsync(u => u.Username == resource.Username || u.Email == resource.Email);
            if (checkUser != null)
            {
                throw new Exception("Username or Email already exists.");
            }


            var user = new Accounts
            {
                Username = resource.Username,
                Email = resource.Email,
                FirstName = resource.FirstName,
                LastName = resource.LastName,
                UserTypeId = 2,
                PasswordSalt = PasswordHasher.GenerateSalt()
            };
            user.PasswordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);

            await _DBContext.Accounts.AddAsync(user, cancellationToken);
            await _DBContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserResource>(user);


        }

        public async Task<string> Login(LoginResource resource, CancellationToken cancellationToken)
        {
            var user = await _DBContext.Accounts
                .FirstOrDefaultAsync(x => x.Username == resource.Username, cancellationToken) ?? throw new Exception("Username not found.");

            if (user.Status == (int)DbEntityStatus.DISABLED || user.Status == (int)DbEntityStatus.MARK_FOR_DELETE)
                throw new Exception("User disabled.");

            var passwordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);
            if (user.PasswordHash != passwordHash)
                throw new Exception("Username or password did not match.");


            var key = _configuration["Authentification:SecretForkey"];
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("userType", user.UserTypeId.ToString()),
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName),
                new Claim("role", "customer")
            };

            var jwtSecurityToken = new JwtSecurityToken(
                    _configuration["Authentification:Issuer"],
                    _configuration["Authentification:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return tokenToReturn;
        }



        public async Task<UserResource> UpdateClient(int userID, ClientResourceForUpdate userResource, CancellationToken cancellationToken)
        {
            var userToBeUpdated = await _DBContext.Accounts
            .FirstOrDefaultAsync(x => x.Id == userID, cancellationToken);

            if (userToBeUpdated == null)
            {
                throw new Exception(nameof(userToBeUpdated));
            }

            _mapper.Map(userResource, userToBeUpdated);
            await _DBContext.SaveChangesAsync(cancellationToken);
            var updatedUser = await GetClientById(userToBeUpdated.Id);

            return _mapper.Map<UserResource>(updatedUser);

        }



        public async Task<bool> DeleteClient(int userID, CancellationToken cancellationToken)
        {

            var user = await _DBContext.Accounts.FirstOrDefaultAsync(x => x.Id == userID, cancellationToken) ?? throw new ArgumentException($"User {userID} not found");

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


        public async Task<UserResource> PatchClient(int userID, JsonPatchDocument patchUser, CancellationToken cancellationToken)
        {
            var userToPatch = await _DBContext.Accounts.FirstOrDefaultAsync(u => u.Id == userID, cancellationToken);
            if (userToPatch == null)
            {
                throw new Exception("User not found");
            }
            patchUser.ApplyTo(userToPatch);
            await _DBContext.SaveChangesAsync(CancellationToken.None);
            return _mapper.Map<UserResource>(userToPatch);
        }
    }




}
