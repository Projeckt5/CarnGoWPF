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
        private IValidator<SecureString> _fakePasswordValidator;
        private IValidator<List<SecureString>> _fakePasswordMatchValidator;
        private IQueryDatabase _fakeQueryDatabase;
        private IApplication _fakeApplication;

        [SetUp]
        public void TestSetup()
        {
            _fakeEmailValidator = Substitute.For<IValidator<string>>();
            _fakePasswordValidator = Substitute.For<IValidator<SecureString>>();
            _fakePasswordMatchValidator = Substitute.For<IValidator<List<SecureString>>>();
            _fakeQueryDatabase = Substitute.For<IQueryDatabase>();
            _fakeApplication = Substitute.For<IApplication>();
            _uut = new UserSignUpViewModel(_fakeEmailValidator,_fakePasswordValidator,_fakePasswordMatchValidator, _fakeQueryDatabase, _fakeApplication);
        }

        public async Task Register_RegisterResetsFlag_IsRegisteringFalse()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(false);
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(false);
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(false);
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            await _uut.RegisterUser();

            Assert.That(_uut.IsRegistering, Is.False);
        }

        [Test]
        public async Task Register_RegisterCancelIfError_AllErrorsNotEmpty()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(false);
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(false);
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(false);
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            await _uut.RegisterUser();

            Assert.That(_uut.AllErrors, Is.Not.Empty);
        }


        [Test]
        public async Task Register_RegisterNavigatesToLogin_GoToLoginPage()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(true);
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(true);
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(true);

            await _uut.RegisterUser();

            _fakeApplication.Received().GoToPage(Arg.Is<ApplicationPage>(page => page == ApplicationPage.LoginPage));
        }


        [Test]
        public void RegisterCommand_RegisterReturnsNewDelegateCommand_CommandReturned()
        {
            var command = _uut.RegisterCommand;

            Assert.That(command, Is.InstanceOf<ICommand>());
        }

        [Test]
        public void RegisterCommand_RegisterCallDbIfValidationTrue_DbGetsEmailPassword()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(true);
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(true);
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(true);

            _uut.RegisterCommand.Execute(null);

            _fakeQueryDatabase.Received().RegisterUserTask(
                Arg.Is<string>(email => email == _uut.Email),
                Arg.Is<SecureString>(pwd => pwd == _uut.PasswordSecureString));
        }

        [Test]
        public void RegisterCommand_RegisterDoesntCallDbIfValidationFalse_DbNotCalled()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(false);
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(false);
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(false);
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            _uut.RegisterCommand.Execute(null);

            _fakeQueryDatabase.DidNotReceive().RegisterUserTask(
                Arg.Any<string>(),
                Arg.Any<SecureString>());
        }


        [Test]
        public void RegisterCommand_DbThrowsAuthenticationException_UserExists()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(true);
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(true);
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(true);
            _fakeQueryDatabase.RegisterUserTask(Arg.Any<string>(), Arg.Any<SecureString>())
                .Throws(new AuthenticationFailedException("Renter already exists"));

            _uut.RegisterCommand.Execute(null);

            Assert.That(_uut.AllErrors.Contains("Renter already exists"));
        }


        [Test]
        public void RegisterCommand_DbThrowsAuthenticationException_IsRegisteringFalse()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(true);
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(true);
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(true);
            _fakeQueryDatabase.RegisterUserTask(Arg.Any<string>(), Arg.Any<SecureString>())
                .Throws(new AuthenticationFailedException("Renter already exists"));

            _uut.RegisterCommand.Execute(null);

            Assert.That(_uut.IsRegistering, Is.False);
        }


        [Test]
        public void RegisterCommand_RegisterCalledTwice_DbQueuedOnlyOnce()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(true);
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(true);
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(true);

            _uut.IsRegistering = true;
            _uut.RegisterCommand.Execute(null);

            _fakeQueryDatabase.DidNotReceive().RegisterUserTask(
                Arg.Any<string>(),
                Arg.Any<SecureString>());
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
        public void NavigateLoginCommand_ApplicationCalled_GoToLoginPage()
        {
            _uut.NavigateLoginCommand.Execute(null);

            _fakeApplication.Received().GoToPage(Arg.Is<ApplicationPage>(page => page == ApplicationPage.LoginPage));
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
        public void PasswordSecureString_SetPropertyTwice_MatchValidatorOnlyCalledOnce(string password)
        {
            var passwordSecureString = password.ConvertToSecureString();
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            _uut.PasswordSecureString = passwordSecureString;
            _uut.PasswordSecureString = passwordSecureString;

            _fakePasswordMatchValidator.Received(1).Validate(Arg.Any<List<SecureString>>());
        }


        [TestCase("1234")]
        [TestCase("")]
        [TestCase("asdf1234")]
        [TestCase("asdf.!#")]
        public void PasswordSecureString_SetPropertyTwice_ValidatorOnlyCalledOnce(string password)
        {
            var passwordSecureString = password.ConvertToSecureString();
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            _uut.PasswordSecureString = passwordSecureString;
            _uut.PasswordSecureString = passwordSecureString;

            _fakePasswordValidator.Received(1).Validate(Arg.Any<SecureString>());
        }


        [TestCase("1234")]
        [TestCase("")]
        [TestCase("asdf1234")]
        [TestCase("asdf.!#")]
        public void PasswordValidateSecureString_SetPropertyTwice_MatchValidatorOnlyCalledOnce(string password)
        {
            var passwordSecureString = password.ConvertToSecureString();
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordMatchValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            _uut.PasswordValidateSecureString = passwordSecureString;
            _uut.PasswordValidateSecureString = passwordSecureString;

            _fakePasswordMatchValidator.Received(1).Validate(Arg.Any<List<SecureString>>());
        }

    }
}