using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using CarnGo.Security;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Prism.Commands;

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
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(false);
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePaswwordValidator.Validate(Arg.Any<SecureString>()).Returns(false);
            _fakePaswwordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(false);
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _uut.IsRegistering = true;

            await _uut.RegisterUser();

            Assert.That(_uut.IsRegistering, Is.False);
        }

        [Test]
        public async Task Register_RegisterCancelIfError_AllErrorsNotEmpty()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(false);
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePaswwordValidator.Validate(Arg.Any<SecureString>()).Returns(false);
            _fakePaswwordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(false);
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            await _uut.RegisterUser();

            Assert.That(_uut.AllErrors, Is.Not.Empty);
        }


        [Test]
        public void AllErrors_AllErrorsSet_AllErrorsOnlySetOnes()
        {
            int invoked = 0;
            var testList = new ObservableCollection<string>() { "test"};
            _uut.PropertyChanged += (sender, args) => ++invoked;
           
            _uut.AllErrors = testList;
            _uut.AllErrors = testList;

            Assert.That(invoked, Is.EqualTo(1));
        }


        [Test]
        public void RegisterCommand_RegisterReturnsNewDelegateCommand_CommandReturned()
        {
            var command = _uut.RegisterCommand;
            
            Assert.That(command,Is.InstanceOf<ICommand>());
        }


        [Test]
        public void NavigateLoginCommand_NavigateReturnsNewDelegateCommand_CommandReturned()
        {
            var command = _uut.NavigateLoginCommand;

            Assert.That(command, Is.InstanceOf<ICommand>());
        }

        [TestCase("MyEmail@email.com")]
        [TestCase("")]
        [TestCase("NotAnEmail")]
        [TestCase("12345")]
        public void ValidateEmail_ValidatorCalled_EmailPropertyPassedCorrectly(string email)
        {
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            _uut.Email = email;
            _uut.Email = email;

            _fakeEmailValidator.Received(1).Validate(email);
        }
        

        [TestCase("1234")]
        [TestCase("")]
        [TestCase("asdf1234")]
        [TestCase("asdf.!#")]
        public void PasswordSecureString_SetPropertyTwice_ValidatorOnlyCalledOnce(string password)
        {
            var passwordsecureString = password.ConvertToSecureString();
            _fakePaswwordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            _uut.PasswordSecureString = passwordsecureString;
            _uut.PasswordSecureString = passwordsecureString;

            _fakePasswordMatchValidator.Received(1).Validate(Arg.Any<List<SecureString>>());
        }


        [TestCase("1234")]
        [TestCase("")]
        [TestCase("asdf1234")]
        [TestCase("asdf.!#")]
        public void PasswordValidateSecureString_SetPropertyTwice_ValidatorOnlyCalledOnce(string password)
        {
            var passwordsecureString = password.ConvertToSecureString();
            _fakePaswwordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            _uut.PasswordValidateSecureString = passwordsecureString;
            _uut.PasswordValidateSecureString = passwordsecureString;

            _fakePasswordMatchValidator.Received(1).Validate(Arg.Any<List<SecureString>>());
        }

    }
}