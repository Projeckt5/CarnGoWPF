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
        public async Task test()
        {
            var expectedEmail = "Test@Test.com";
            var expectedPassword = "Test1234".ConvertToSecureString();

            await _uut.RegisterUserTask(expectedEmail, expectedPassword);

            await _fakeDbContext.Received().AddUser(
                Arg.Is<User>(u => u.Email == expectedEmail && u.Password == expectedPassword.ConvertToString()));
        }
    }
}
