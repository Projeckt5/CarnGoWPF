using CarnGo.Database;
using CarnGo.Database.Models;
using CarnGo.Security;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo.Test.Unit
{
    [TestFixture]
    public class RealDatabaseQuerierTest
    {
        private IQueryDatabase _uut;
        private IAppToDbModelConverter _fakeAppToDbModelConverter;
        private IDbToAppModelConverter _fakeDbToAppModelConverter;
        private IAppDbContext _fakeDbContext;
        [SetUp]
        public void UnitTestSetup()
        {
            _fakeAppToDbModelConverter = Substitute.For<IAppToDbModelConverter>();
            _fakeDbToAppModelConverter = Substitute.For<IDbToAppModelConverter>();
            _fakeDbContext = Substitute.For<IAppDbContext>();
            _uut = new RealDatabaseQuerier(_fakeDbContext, _fakeDbToAppModelConverter, _fakeAppToDbModelConverter);
        }

        [Test]
        public async Task RegisterUserTask_RegisterUserCallsAddUserOnDbContext_EmailAndPasswordCorrect()
        {
            var expectedEmail = "Test@Test.com";
            var expectedPassword = "Test1234".ConvertToSecureString();

            await _uut.RegisterUserTask(expectedEmail, expectedPassword);

            await _fakeDbContext.Received().AddUser(
                Arg.Is<User>(u => u.Email == expectedEmail && u.Password == expectedPassword.ConvertToString()));
        }

        [Test]
        public async Task GetUserTask_GetUserCallsGetUserOnDbContext_EmailAndPasswordCorrect()
        {
            var expectedEmail = "Test@Test.com";
            var expectedPassword = "Test1234".ConvertToSecureString();

            await _uut.GetUserTask(expectedEmail, expectedPassword);

            await _fakeDbContext.Received().GetUser(
                Arg.Is<string>(email => email == expectedEmail),
                Arg.Is<string>(pwd =>pwd == expectedPassword.ConvertToString()));
        }

        [Test]
        public async Task GetUserTask_GetUserCallsConverter_EmailAndPasswordCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var expectedPassword = "Test1234".ConvertToSecureString();
            _fakeDbToAppModelConverter.Convert(Arg.Any<User>()).Returns(testUser);

            await _uut.GetUserTask(testUser.Email, expectedPassword);

            await _fakeDbContext.Received().GetUser(
                Arg.Is<string>(email => email == testUser.Email),
                Arg.Is<string>(pwd => pwd == expectedPassword.ConvertToString()));
        }

        [Test]
        public async Task GetUserTask_GetUserReturnsExpectedUserModel_UserCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var expectedPassword = "Test1234".ConvertToSecureString();
            _fakeDbToAppModelConverter.Convert(Arg.Any<User>()).Returns(testUser);

            var userResult = await _uut.GetUserTask(testUser.Email, expectedPassword);

            Assert.That(userResult,Is.EqualTo(testUser));
        }

        [Test]
        public async Task GetUserMessagesTask_GetUserMessagesCallsConverter_MessageCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var testMessageList = new List<MessageModel>()
            {
                TestModelFactory.CreateMessageModel("Test",MessageType.LessorMessage)
            };
            _fakeDbToAppModelConverter.Convert(Arg.Any<List<Message>>()).Returns(testMessageList);

            await _uut.GetUserMessagesTask(testUser);

            await _fakeDbContext.Received().GetUser(
                Arg.Is<string>(email => email == testUser.Email),
                Arg.Any<Guid>());
        }

        [Test]
        public async Task GetUserMessagesTask_GetUserMessagesCallsDbContext_UserCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var testMessageList = new List<MessageModel>()
            {
                TestModelFactory.CreateMessageModel("Test",MessageType.LessorMessage)
            };
            _fakeDbToAppModelConverter.Convert(Arg.Any<List<Message>>()).Returns(testMessageList);
            _fakeDbContext.GetUser(Arg.Any<string>(), Arg.Any<Guid>()).Returns(new User()
            {
                Email = testUser.Email,
            });

            await _uut.GetUserMessagesTask(testUser);

            await _fakeDbContext.Received().GetMessages(Arg.Is<User>(u => u.Email == testUser.Email));
        }


        [Test]
        public async Task GetUserMessagesTask_GetUserMessagesReturnsExpectedUserModel_UserCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var testMessageList = new List<MessageModel>()
            {
                TestModelFactory.CreateMessageModel("Test",MessageType.LessorMessage)
            };
            _fakeDbToAppModelConverter.Convert(Arg.Any<List<Message>>()).Returns(testMessageList);

            var userMessageResult = await _uut.GetUserMessagesTask(testUser);

            Assert.That(userMessageResult, Is.EqualTo(testMessageList));
        }
    }
}
