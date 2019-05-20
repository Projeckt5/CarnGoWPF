using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Security;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CarnGo.Integrationtest
{
    [TestFixture]
    public class RealDbQueryAndDbContextIntegration
    {
        private RealDatabaseQuerier _dbQuerier;
        private AppDbContext _dbContextToIntegrate;
        private ApptoDbModelConverter _appToDbModelConverter;
        private DbToAppModelConverter _dbToAppModelConverter;

        [SetUp]
        public void TestSetup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
            _dbContextToIntegrate = new AppDbContext(options);
            _appToDbModelConverter = new ApptoDbModelConverter();
            _dbToAppModelConverter = new DbToAppModelConverter();
            _dbQuerier = new RealDatabaseQuerier(_dbContextToIntegrate,_dbToAppModelConverter,_appToDbModelConverter);
        }

        [TestCase("Hello@Hello","123456")]
        [TestCase("a@a","asd123")]
        [TestCase("asd@123","asd123")]
        public async Task CreateUser_UserCreatedInDb_EmailCorrect(string email, string password)
        {
            var pwd = password.ConvertToSecureString();

            await _dbQuerier.RegisterUserTask(email, pwd);

            Assert.That(await _dbContextToIntegrate.Users.AnyAsync());
        }
    }
}