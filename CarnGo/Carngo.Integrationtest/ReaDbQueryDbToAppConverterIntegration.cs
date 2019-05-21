using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Database.Models;
using CarnGo.Security;
using NSubstitute;
using NUnit.Framework;

namespace CarnGo.Integrationtest
{
    [TestFixture]
    public class ReaDbQueryDbToAppConverterIntegration
    {

        private RealDatabaseQuerier _dbQuerier;
        private IAppDbContext _fakeDbContext;
        private ApptoDbModelConverter _appToDbModelConverter;
        private DbToAppModelConverter _converterToIntegrate;

        [SetUp]
        public void TestSetup()
        {
            _fakeDbContext = Substitute.For<IAppDbContext>();
            _converterToIntegrate = new DbToAppModelConverter();
            _appToDbModelConverter = new ApptoDbModelConverter();
            _dbQuerier = new RealDatabaseQuerier(_fakeDbContext,
                _converterToIntegrate,
                _appToDbModelConverter);
        }

        [TestCase("Email", "Password")]
        [TestCase("email@email", "1234")]
        [TestCase("1@2.com", "123ikasd")]
        public async Task GetUser_UserFromDbConverted_UserReturnedCorrectly(string email, string password)
        {
            var pwd = password.ConvertToSecureString();
            _fakeDbContext.GetUser(Arg.Any<string>(), Arg.Any<string>())
                .Returns(new User()
                {
                    Email = email,
                    Password = password
                });

            await _dbQuerier.RegisterUserTask(email, pwd);
            var userResult = await _dbQuerier.GetUserTask(email, pwd);

            Assert.That(userResult.Email, Is.EqualTo(email));
        }
    }
}