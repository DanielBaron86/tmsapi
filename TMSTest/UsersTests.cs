using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using TasksAPI.DataBaseContext;
using TasksAPI.Entities;
using TasksAPI.Models;
using TasksAPI.Profiles;
using TasksAPI.Services;
using Xunit.Abstractions;

namespace TMSTest
{
    public class UsersTests
    {

        private readonly ITestOutputHelper output;

        public UsersTests(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public async void TestUSerRegistrationAndLogin()
        {
            var dbOptions = new DbContextOptionsBuilder<DatabaseConnectContext>()
            .UseInMemoryDatabase(databaseName: "TMSAPI")
            .Options;

            var mockIconf = new Mock<IConfiguration>();
            mockIconf.SetupGet(m => m[It.Is<string>(s => s == "Authentification:SecretForkey")]).Returns("g%3y+Pv8fZmT)Apr$8aGuBfuM$#AqRb$");
            mockIconf.SetupGet(m => m[It.Is<string>(s => s == "Authentification:Issuer")]).Returns("http://localhost");
            mockIconf.SetupGet(m => m[It.Is<string>(s => s == "Authentification:Audience")]).Returns("tmsapi");

            var mockMapper = new Mock<IMapper>();

            var userToRegister = new RegisterResource("daniel", "daniel@daniel.com", "asdasd1234", "Baron", "Daniel", 2);

            using (var contextDB = new DatabaseConnectContext(dbOptions))
            {
                UserService userService = new UserService(contextDB, mockIconf.Object, mockMapper.Object);
                await userService.Register(userToRegister, CancellationToken.None);
                var loginResult = await userService.Login(new LoginResource("daniel", "asdasd1234"), CancellationToken.None);
                var isValid = userService.ValidateToken(loginResult);

                Assert.True(isValid);
            }
        }
    }
}