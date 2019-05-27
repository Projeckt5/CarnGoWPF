using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Security;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;

namespace CarnGo.Integrationtest
{
    [TestFixture]
    public class RealDbQueryAndDbContextIntegration
    {
        private RealDatabaseQuerier _dbQuerier;
        private IDbContextFactory _dbContextFactory;
        private ApptoDbModelConverter _appToDbModelConverter;
        private DbToAppModelConverter _dbToAppModelConverter;
        private DbContextOptions<AppDbContext> _options;

        [SetUp]
        public void TestSetup()
        {
            _appToDbModelConverter = new ApptoDbModelConverter();
            _dbToAppModelConverter = new DbToAppModelConverter();
            _dbContextFactory = new TestDbContextFactory();
            _dbQuerier = new RealDatabaseQuerier(_dbContextFactory,
                _dbToAppModelConverter,
                _appToDbModelConverter);
        }

        [TearDown]
        public void TestTearDown()
        {
            ((AppDbContext)_dbContextFactory.GetContext()).Database.EnsureDeleted();
        }

        [TestCase("Hello@Hello","123456")]
        [TestCase("a@a","asd123")]
        [TestCase("asd@123","asd123")]
        public async Task CreateUser_UserCreatedInDb_EmailCorrect(string email, string password)
        {
            var pwd = password.ConvertToSecureString();

            await _dbQuerier.RegisterUserTask(email, pwd);

            using (var db = (AppDbContext) _dbContextFactory.GetContext())
            {

                var anyUser = await db.Users.AnyAsync();
                Assert.That(anyUser);
            }
        }

        [TestCase("Hello@Hello", "123456")]
        [TestCase("a@a", "asd123")]
        [TestCase("asd@123", "asd123")]
        public async Task GetUser_GetUserFromDb_EmailCorrect(string email, string password)
        {
            var pwd = password.ConvertToSecureString();
            await _dbQuerier.RegisterUserTask(email, pwd);

            var userResult = await _dbQuerier.GetUserTask(email, pwd);

            Assert.That(userResult.Email, Is.EqualTo(email));
        }

        [TestCase("Hello@Hello", "123456")]
        [TestCase("a@a", "asd123")]
        [TestCase("asd@123", "asd123")]
        public async Task GetUser_GetUserFromDbWithUserModel_EmailCorrect(string email, string password)
        {
            var pwd = password.ConvertToSecureString();
            await _dbQuerier.RegisterUserTask(email, pwd);
            var user = await _dbQuerier.GetUserTask(email, pwd);

            var userResult = await _dbQuerier.GetUserTask(user);

            Assert.That(userResult.Email, Is.EqualTo(email));
        }

        [TestCase("Hello@Hello", "123456")]
        [TestCase("a@a", "asd123")]
        [TestCase("asd@123", "asd123")]
        public async Task GetUser_SetAuthorizationString_ThrowsAuthException(string email, string password)
        {
            var pwd = password.ConvertToSecureString();
            await _dbQuerier.RegisterUserTask(email, pwd);
            var user = await _dbQuerier.GetUserTask(email, pwd);
            user.AuthenticationString = Guid.NewGuid();

            Assert.ThrowsAsync<AuthenticationFailedException>(async ()=>await _dbQuerier.GetUserTask(user));
        }

        [TestCase("Hello", "World")]
        [TestCase("Mads", "Madsen")]
        [TestCase("Karen", "Jensen")]
        public async Task UpdateUser_SetFirstAndLastName_FirstAndLastCorrectOnGet(string first, string last)
        {
            var pwd = "123asd".ConvertToSecureString();
            var email = $"{first}@{last}";
            await _dbQuerier.RegisterUserTask(email, pwd);
            var user = await _dbQuerier.GetUserTask(email, pwd);
            user.FirstName = first;
            user.LastName = last;

            await _dbQuerier.UpdateUser(user);
            var userResult = await _dbQuerier.GetUserTask(user);

            Assert.That(userResult.FirstName, Is.EqualTo(first));
            Assert.That(userResult.LastName, Is.EqualTo(last));
        }

        [Test]
        public async Task UpdateUser_SetAddress_AddressCorrectOnGet()
        {
            var pwd = "123asd".ConvertToSecureString();
            var email = "test@test";
            await _dbQuerier.RegisterUserTask(email, pwd);
            var user = await _dbQuerier.GetUserTask(email, pwd);
            user.Address = "Test Address";

            await _dbQuerier.UpdateUser(user);
            var userResult = await _dbQuerier.GetUserTask(user);

            Assert.That(userResult.Address, Is.EqualTo(user.Address));
        }


        [Test]
        public async Task AddUserMessages_UserMessagesInDb_DbContainsAnyMessage()
        {
            var pwd = "123asd".ConvertToSecureString();
            var renterEmail = "renter@test";
            var lessorEmail = "lessor@test";
            await _dbQuerier.RegisterUserTask(renterEmail, pwd);
            await _dbQuerier.RegisterUserTask(lessorEmail, pwd);
            var renterUser = await _dbQuerier.GetUserTask(renterEmail, pwd);
            var lessorUser = await _dbQuerier.GetUserTask(lessorEmail, pwd);
            var carProfile = TestModelFactory.CreateCarProfile();
            var testMessage = new MessageFromRenterModel(renterUser, lessorUser, carProfile, "TestMessage")
            {
                Sender = renterUser,
                Receiver = lessorUser
            };

            await _dbQuerier.AddUserMessage(testMessage);

            Assert.That(((AppDbContext)_dbContextFactory.GetContext()).Messages.Any());
        }

        [Test]
        public async Task AddUserMessages_UserMessageJunctionInDb_ContainsAnyMessageAndUser()
        {
            var pwd = "123asd".ConvertToSecureString();
            var renterEmail = "renter@test";
            var lessorEmail = "lessor@test";
            await _dbQuerier.RegisterUserTask(renterEmail, pwd);
            await _dbQuerier.RegisterUserTask(lessorEmail, pwd);
            var renterUser = await _dbQuerier.GetUserTask(renterEmail, pwd);
            var lessorUser = await _dbQuerier.GetUserTask(lessorEmail, pwd);
            var carProfile = TestModelFactory.CreateCarProfile();
            var testMessage = new MessageFromRenterModel(renterUser, lessorUser, carProfile, "TestMessage")
            {
                Sender = renterUser,
                Receiver = lessorUser
            };

            await _dbQuerier.AddUserMessage(testMessage);

            Assert.That(((AppDbContext)_dbContextFactory.GetContext()).MessagesWithUsersJunction.Any());
        }

        [Test]
        public async Task GetUserMessages_GetMessagesForRenter_ContainsCorrectMessage()
        {
            var pwd = "123asd".ConvertToSecureString();
            var renterEmail = "renter@test";
            var lessorEmail = "lessor@test";
            await _dbQuerier.RegisterUserTask(renterEmail, pwd);
            await _dbQuerier.RegisterUserTask(lessorEmail, pwd);
            var renterUser = await _dbQuerier.GetUserTask(renterEmail, pwd);
            var lessorUser = await _dbQuerier.GetUserTask(lessorEmail, pwd);
            var carProfile = TestModelFactory.CreateCarProfile();
            var testMessage = new MessageFromRenterModel(renterUser, lessorUser, carProfile, "TestMessage")
            {
                Sender = renterUser,
                Receiver = lessorUser
            };

            await _dbQuerier.AddUserMessage(testMessage);
            var userMessagesResult = await _dbQuerier.GetUserMessagesTask(renterUser,10);

            Assert.That(userMessagesResult.Any(model => model.Message == "TestMessage"));
        }


        [Test]
        public async Task UpdateUserMessages_GetMessagesForRenter_ContainsCorrectMessage()
        {
            var pwd = "123asd".ConvertToSecureString();
            var renterEmail = "renter@test";
            var lessorEmail = "lessor@test";
            await _dbQuerier.RegisterUserTask(renterEmail, pwd);
            await _dbQuerier.RegisterUserTask(lessorEmail, pwd);
            var renterUser = await _dbQuerier.GetUserTask(renterEmail, pwd);
            var lessorUser = await _dbQuerier.GetUserTask(lessorEmail, pwd);
            var carProfile = TestModelFactory.CreateCarProfile();
            var testMessage = new MessageFromRenterModel(renterUser, lessorUser, carProfile, "TestMessage")
            {
                Sender = renterUser,
                Receiver = lessorUser
            };

            await _dbQuerier.AddUserMessage(testMessage);
            var userMessages = await _dbQuerier.GetUserMessagesTask(renterUser, 10);
            userMessages[0].Message = "UpdatedMessage";
            await _dbQuerier.UpdateUserMessagesTask(userMessages);
            var userMessagesResult = await _dbQuerier.GetUserMessagesTask(renterUser, 10);

            Assert.That(userMessagesResult.Any(model => model.Message == "UpdatedMessage"));
        }
    }
}