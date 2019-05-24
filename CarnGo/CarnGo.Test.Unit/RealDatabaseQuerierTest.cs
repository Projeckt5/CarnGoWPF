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
using System.Windows.Navigation;

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
        #region RegisterUserTask
        [Test]
        public async Task RegisterUserTask_RegisterUserCallsAddUserOnDbContext_EmailAndPasswordCorrect()
        {
            var expectedEmail = "Test@Test.com";
            var expectedPassword = "Test1234".ConvertToSecureString();

            await _uut.RegisterUserTask(expectedEmail, expectedPassword);

            await _fakeDbContext.Received().AddUser(
                Arg.Is<User>(u => u.Email == expectedEmail && u.Password == expectedPassword.ConvertToString()));
        } 
        #endregion
        #region GetUserTask

        [Test]
        public async Task GetUserTask_GetUserCallsGetUserOnDbContext_EmailAndPasswordCorrect()
        {
            var expectedEmail = "Test@Test.com";
            var expectedPassword = "Test1234".ConvertToSecureString();

            await _uut.GetUserTask(expectedEmail, expectedPassword);

            await _fakeDbContext.Received().GetUser(
                Arg.Is<string>(email => email == expectedEmail),
                Arg.Is<string>(pwd => pwd == expectedPassword.ConvertToString()));
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

            Assert.That(userResult, Is.EqualTo(testUser));
        } 
        #endregion
        #region GetUserMessageTask

        [Test]
        public async Task GetUserMessagesTask_GetUserMessagesCallsConverter_MessageCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var testMessageList = new List<MessageModel>()
            {
                TestModelFactory.CreateMessageModel("Test",MessageType.LessorMessage)
            };
            _fakeDbToAppModelConverter.Convert(Arg.Any<List<Message>>()).Returns(testMessageList);

            await _uut.GetUserMessagesTask(testUser,10);

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

            await _uut.GetUserMessagesTask(testUser, 10);

            await _fakeDbContext.Received().GetMessages(Arg.Is<User>(u => u.Email == testUser.Email),Arg.Any<List<Message>>(),Arg.Any<int>());
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

            var userMessageResult = await _uut.GetUserMessagesTask(testUser, 10);

            Assert.That(userMessageResult, Is.EqualTo(testMessageList));
        }
        #endregion

        #region UpdateUser
        [Test]
        public async Task UpdateUserTask_UpdateUserCallsConverter_UserCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();

            await _uut.UpdateUser(testUser);

            _fakeAppToDbModelConverter.Received().Convert(Arg.Is<UserModel>(um => um == testUser));
        }

        [Test]
        public async Task UpdateUserTask_UpdateUserCallsDbContext_UserCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();

            await _uut.UpdateUser(testUser);

            await _fakeDbContext.Received().UpdateUserInformation(Arg.Any<User>());
        }

        #endregion

        #region UpdateUser
        [Test]
        public async Task UpdateUserMessagesTask_UpdateUserCallsConverter_MessagesCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var testMessageModels = new List<MessageModel> {TestModelFactory.CreateMessageModel()};
            _fakeAppToDbModelConverter.Convert(Arg.Any<List<MessageModel>>()).Returns(new List<Message>());

            await _uut.UpdateUserMessagesTask(testMessageModels);

            _fakeAppToDbModelConverter.Received().Convert(Arg.Is<List<MessageModel>>(m => m == testMessageModels));
        }

        [Test]
        public async Task UpdateUserMessagesTask_UpdateUserCallsDbContext_UserCorrect()
        {
            var testMessages = new List<Message>()
            {
                new Message()
                {
                    TheMessage = "TestMsg1"
                },
                new Message()
                {
                    TheMessage = "TestMsg2"
                }
            };
            var testUser = TestModelFactory.CreateUserModel();
            var testMessageModels = new List<MessageModel> { TestModelFactory.CreateMessageModel() };
            _fakeAppToDbModelConverter.Convert(Arg.Any<List<MessageModel>>()).Returns(testMessages);

            await _uut.UpdateUserMessagesTask(testMessageModels);

            await _fakeDbContext.Received(2).UpdateMessage(Arg.Is<Message>(msg => testMessages.Contains(msg)));
        }

        #endregion

        #region GetUserTask (GUID)

        [Test]
        public async Task GetUserTask_GetUserCallsGetUserOnDbContext_EmailAndGuidCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            testUser.AuthenticationString = Guid.NewGuid();

            await _uut.GetUserTask(testUser);

            await _fakeDbContext.Received().GetUser(Arg.Is<string>(s => s == testUser.Email), 
                Arg.Is<Guid>(guid => guid == testUser.AuthenticationString));
        }

        [Test]
        public async Task GetUserTask_GetUserCallsConverter_EmailAndGuidCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            _fakeDbContext.GetUser(Arg.Any<string>(), Arg.Any<Guid>()).Returns(new User()
            {
                Email = testUser.Email
            });

            await _uut.GetUserTask(testUser);

            _fakeDbToAppModelConverter.Received().Convert(Arg.Is<User>(u => u.Email == testUser.Email));
        }

        [Test]
        public async Task GetUserTask_GetUserReturnsUserModel_UserCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            testUser.AuthenticationString = Guid.NewGuid();
            _fakeDbToAppModelConverter.Convert(Arg.Any<User>()).Returns(testUser);

            var userResult = await _uut.GetUserTask(testUser);

            Assert.That(userResult, Is.EqualTo(testUser));
        }
        #endregion


        #region UpdateCarProfile
        [Test]
        public async Task UpdateCarProfileTask_CallsConverter_ProfileCorrect()
        {
            var testCarProfileModel = TestModelFactory.CreateCarProfile();
            _fakeAppToDbModelConverter.Convert(Arg.Any<CarProfileModel>()).Returns(new CarProfile());

            await _uut.UpdateCarProfileTask(testCarProfileModel);

            _fakeAppToDbModelConverter.Received().Convert(Arg.Is<CarProfileModel>(m => m == testCarProfileModel));
        }

        [Test]
        public async Task UpdateCarProfileTask_CallsDbContext_UpdateCarProfileTask_CallsConverter_ProfileCorrect()
        {
            var testCarProfileModel = TestModelFactory.CreateCarProfile();
            _fakeAppToDbModelConverter.Convert(Arg.Any<CarProfileModel>()).Returns(new CarProfile()
            {
                RegNr = testCarProfileModel.RegNr
            });

            await _uut.UpdateCarProfileTask(testCarProfileModel);

            await _fakeDbContext.Received().UpdateCarProfile(Arg.Is<CarProfile>(car => car.RegNr == testCarProfileModel.RegNr ));
        }

        #endregion

        #region GetCarProfileTask

        [Test]
        public async Task GetCarProfilesTask_CallsDbContextForUser_CallsDbWithCorrectUserModel()
        {
            var testUser = TestModelFactory.CreateUserModel();

            var dbCars = await _uut.GetCarProfilesTask(testUser);

            await _fakeDbContext.Received().GetUser(Arg.Is<string>(s => s == testUser.Email), Arg.Any<Guid>());
        }

        [Test]
        public async Task GetCarProfilesTask_CallsDbContextForAllCars_EmailCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var fakeUser = new User()
            {
                Email = testUser.Email
            };
            _fakeDbContext.GetUser(Arg.Any<string>(), Arg.Any<Guid>()).Returns(fakeUser);

            await _uut.GetCarProfilesTask(testUser);

            await _fakeDbContext.Received().GetAllCars(Arg.Is<User>(u => u.Email == testUser.Email));
        }

        [Test]
        public async Task GetCarProfilesTask_ReturnsCarProfile_EmailCorrect()
        {
            var testUser = TestModelFactory.CreateUserModel();
            var fakeUser = new User()
            {
                Email = testUser.Email
            };
            var fakeCarProfiles = new List<CarProfile>()
            {
                new CarProfile()
                {
                    OwnerEmail = testUser.Email,
                }
            };
            _fakeDbContext.GetUser(Arg.Any<string>(), Arg.Any<Guid>()).Returns(fakeUser);
            _fakeDbContext.GetAllCars(Arg.Any<User>()).Returns(fakeCarProfiles);

            await _uut.GetCarProfilesTask(testUser);
            
            _fakeDbToAppModelConverter.Received().Convert(Arg.Any<List<CarProfile>>());
        }
        #endregion
    }
}
