using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using CarnGo.Security;
using NSubstitute;
using NUnit.Framework;

namespace CarnGo.Test.Unit.ViewModels
{
    [TestFixture]
    public class UserSignUpViewModelTest
    {
        private UserSignUpViewModel _uut;
        private IValidator<string> _fakeEmailValidator;
        private IValidator<SecureString> _fakePaswwordValidator;
        private IValidator<List<SecureString>> _fakePasswordMatchValidator;

        [SetUp]
        public void TestSetup()
        {
            _fakeEmailValidator = Substitute.For<IValidator<string>>();
            _fakePaswwordValidator = Substitute.For<IValidator<SecureString>>();
            _fakePasswordMatchValidator = Substitute.For<IValidator<List<SecureString>>>();
            _uut = new UserSignUpViewModel(_fakeEmailValidator,_fakePaswwordValidator,_fakePasswordMatchValidator);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Register_RegisterResetsFlag_IsRegisteringFalse(bool validationResult)
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(validationResult);
            _fakePaswwordValidator.Validate(Arg.Any<SecureString>()).Returns(validationResult);
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(validationResult);
            _uut.IsRegistering = true;

            await _uut.RegisterUser();

            Assert.That(_uut.IsRegistering, Is.False);
        }


        [Test]
        public async Task Register_RegisterCancelIfError_AllErrorsNotEmpty()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(false);

            await _uut.RegisterUser();

            Assert.That(_uut.AllErrors, Is.Not.Empty);
        }
    }
}