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

namespace CarnGo.Test.Unit
{
    [TestFixture]
    public class LoginViewModelTest
    {
        private LoginPageViewModel _uut;
        private IValidator<string> _fakeEmailValidator;
        private IValidator<SecureString> _fakePasswordValidator;
        private IQueryDatabase _fakeQueryDatabase;
        private IApplication _fakeApplication;

        [SetUp]
        public void TestSetup()
        {
            _fakeEmailValidator = Substitute.For<IValidator<string>>();
            _fakePasswordValidator = Substitute.For<IValidator<SecureString>>();
            _fakeQueryDatabase = Substitute.For<IQueryDatabase>();
            _fakeApplication = Substitute.For<IApplication>();
            _uut = new LoginPageViewModel(_fakeEmailValidator, _fakePasswordValidator, _fakeQueryDatabase, _fakeApplication);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Login_LoginResetsFlag_IsLoginFalse(bool validationResult)
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(false);
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(false);
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            

            await _uut.Login();

            Assert.That(_uut.IsLogin, Is.False);
        }

       

        [Test]
        public async Task Login_LoginCancelIfError_AllErrorsNotEmpty()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(false);
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(false);
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            

            await _uut.Login();

            Assert.That(_uut.AllErrors, Is.Not.Empty);
        }

        [Test]
        public async Task Login_LoginNavigatesToLogin_GoToStartpage()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(true);
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(true);
            

            await _uut.Login();

            _fakeApplication.Received().GoToPage(Arg.Is<ApplicationPage>(page => page == ApplicationPage.StartPage));
        }

        [Test]
        public void LoginCommand_RegisterReturnsNewDelegateCommand_CommandReturned()
        {
            var command = _uut.LoginCommand;

            Assert.That(command, Is.InstanceOf<ICommand>());
        }

        [Test]
        public void NavigateStartPageCommand_ApplicationCalled_GoToStartPage()
        {
            _uut.NavigateStartPageCommand.Execute(null);

            _fakeApplication.Received().GoToPage(Arg.Is<ApplicationPage>(page => page == ApplicationPage.StartPage));
        }

        [Test]
        public void NavigateStartPageCommand_ApplicationCalled_GoToRegisterUser()
        {
            _uut.NavigateUserSignupCommand.Execute(null);

            _fakeApplication.Received().GoToPage(Arg.Is<ApplicationPage>(page => page == ApplicationPage.UserSignUpPage));
        }
        [Test]
        public void AllErrors_AllErrorsSet_AllErrorsOnlySetOnes()
        {
            int invoked = 0;
            var testList = new ObservableCollection<string>() { "test" };
            _uut.PropertyChanged += (sender, args) => ++invoked;

            _uut.AllErrors = testList;
            _uut.AllErrors = testList;

            Assert.That(invoked, Is.EqualTo(1));
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

        [TestCase("MyEmail@email.com")]
        [TestCase("")]
        [TestCase("NotAnEmail")]
        [TestCase("12345")]
        public void EmailString_SetPropertyTwice_ValidatorOnlyCalledOnce(string email)
        {
            
            _fakeEmailValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });

            _uut.Email = email;
            _uut.Email = email;

            _fakeEmailValidator.Received(1).Validate(Arg.Any<string>());
        }


        [TestCase("1234")]
        [TestCase("")]
        [TestCase("asdf1234")]
        [TestCase("asdf.!#")]
        public void PasswordSecureString_SetPropertyTwice_ValidatorOnlyCalledOnce(string password)
        {
            var passwordSecureString = password.ConvertToSecureString();
            _fakePasswordValidator.ValidationErrorMessages.Returns(new List<string>() { "test" });
            
            _uut.PasswordSecureString = passwordSecureString;
            _uut.PasswordSecureString = passwordSecureString;

            _fakePasswordValidator.Received(1).Validate(Arg.Any<SecureString>());
        }

    }

}
