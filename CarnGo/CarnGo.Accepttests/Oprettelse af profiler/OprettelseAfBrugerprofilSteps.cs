using System;
using System.Collections.Generic;
using System.Security;
using CarnGo.Security;
using NSubstitute;
using TechTalk.SpecFlow;

namespace CarnGo.Accepttests
{
    [Binding]
    public class OprettelseAfBrugerprofilSteps
    {
        private UserSignUpViewModel _uut;
        private IValidator<string> _fakeEmailValidator;
        private IValidator<SecureString> _fakePasswordValidator;
        private IValidator<List<SecureString>> _fakePasswordMatchValidator;
        private IQueryDatabase _fakeQueryDatabase;
        private IApplication _fakeApplication;

        [Given(@"bruger ønsker at oprette en profil")]
        public void GivetBrugerOnskerAtOpretteEnProfil()
        {
            _fakeEmailValidator = Substitute.For<IValidator<string>>();
            _fakePasswordValidator = Substitute.For<IValidator<SecureString>>();
            _fakePasswordMatchValidator = Substitute.For<IValidator<List<SecureString>>>();
            _fakeQueryDatabase = Substitute.For<IQueryDatabase>();
            _fakeApplication = Substitute.For<IApplication>();
            _uut = new UserSignUpViewModel(_fakeEmailValidator, _fakePasswordValidator,
                _fakePasswordMatchValidator, _fakeQueryDatabase, _fakeApplication);
        }
        
        [When(@"bruger indtaster sin email og password")]
        public void NarBrugerIndtasterSinEmailOgPassword()
        {
            _fakeEmailValidator.Validate(Arg.Any<string>()).Returns(true);
            _fakePasswordValidator.Validate(Arg.Any<SecureString>()).Returns(true);
            _fakePasswordMatchValidator.Validate(Arg.Any<List<SecureString>>()).Returns(true);
        }
        
        [When(@"bruger trykker på knappen Register")]
        public void NarBrugerTrykkerPaKnappenRegister()
        {
            _uut.RegisterCommand.Execute(null);
        }
        
        [Then(@"bliver oplysningerne gemt i databasen")]
        public void SaBliverOplysningerneGemtIDatabasen()
        {
            _fakeQueryDatabase.Received().RegisterUserTask(
                Arg.Is<string>(email => email == _uut.Email),
                Arg.Is<SecureString>(pwd => pwd == _uut.PasswordSecureString));
        }
        
        [Then(@"bruger ledes til Login page")]
        public void SaBrugerLedesTilLoginPage()
        {
            _fakeApplication.Received().GoToPage(ApplicationPage.LoginPage);
        }
    }
}
