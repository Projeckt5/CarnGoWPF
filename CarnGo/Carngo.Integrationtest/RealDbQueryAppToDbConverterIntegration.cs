using System.Security;
using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Database.Models;
using CarnGo.Security;
using NSubstitute;
using NUnit.Framework;

namespace CarnGo.Integrationtest
{
    [TestFixture]
    public class RealDbQueryAppToDbConverterIntegration
    {

        private RealDatabaseQuerier _dbQuerier;
        private IAppDbContext _fakeDbContext;
        private ApptoDbModelConverter _converterToIntegrate;
        private IDbToAppModelConverter _fakeDbToAppModelConverter;
        private IDbContextFactory _dbContextFactory;

        [SetUp]
        public void TestSetup()
        {
            _fakeDbContext = Substitute.For<IAppDbContext>();
            _fakeDbToAppModelConverter = Substitute.For<IDbToAppModelConverter>();
            _converterToIntegrate = new ApptoDbModelConverter();
            _dbContextFactory = Substitute.For<IDbContextFactory>();
            _dbContextFactory.GetContext().Returns(_fakeDbContext);
            _dbQuerier = new RealDatabaseQuerier(_dbContextFactory,
                _fakeDbToAppModelConverter,
                _converterToIntegrate);
        }

        [TestCase("Email", "Password")]
        [TestCase("email@email", "1234")]
        [TestCase("1@2.com", "123ikasd")]
        public async Task RegisterUser_DbContextReceivedAddUser_UserCredentialsCorrect(string email,string password)
        {
            var pwd = password.ConvertToSecureString();

            await _dbQuerier.RegisterUserTask(email, pwd);

            await _fakeDbContext.Received()
                .AddUser(Arg.Is<User>(u => u.Email == email && u.Password == password));
        }

        [TestCase("Hello world")]
        [TestCase("Goodbye")]
        [TestCase("123TestMessage")]
        public async Task AddUserMessage_DbContextReceivedAddMessage_MessageCorrect(string message)
        {
            var pwd = "123asd".ConvertToSecureString();
            var email = "123@asd";
            await _dbQuerier.RegisterUserTask(email, pwd);
            var messageModel = TestModelFactory.CreateMessageModel(message, MessageType.LessorMessage);

            await _dbQuerier.AddUserMessage(messageModel);
            

            await _fakeDbContext.Received()
                .AddMessage(Arg.Is<Message>(msg => msg.TheMessage == message));
        }

    }
}