using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using CarnGo.Database.Models;
using CarnGo.Security.Validation;
using CarnGo.Security;
using CarnGo;

namespace CarnGo.Integrationtest
{
    [TestFixture]
    public class Step1
    {
        // model
        public CarEquipment _carEquipment;
        public CarProfile _carProfile;
        public DayThatIsRented _dayThatIsRented;
        public PossibleToRentDay _possibleToRentDay;
        public Message _message;
        public MessagesWithUsers _messagesWithUsers;
        public User _user;

        //security
        public CarAgeValidator _carAgeValidator;
        public EmailValidator _emailValidator;
        public LicensePlateValidator _licensePlateValidator;
        public NameValidator _nameValidator;
        public PasswordMatchValidator _passwordMatchValidator;
        public PasswordValidator _passwordValidator;
        public PriceValidator _priceValidator;


        [SetUp]
        public void Setup()
        {
            // model
            _carEquipment = new CarEquipment();
            _carProfile = new CarProfile();
            _dayThatIsRented = new DayThatIsRented();
            _possibleToRentDay = new PossibleToRentDay();
            _message = new Message();
            _messagesWithUsers = new MessagesWithUsers();
            _user = new User();

            //security
            _carAgeValidator = new CarAgeValidator();
            _emailValidator = new EmailValidator();
            _licensePlateValidator = new LicensePlateValidator();
            _nameValidator = new NameValidator();
            _passwordMatchValidator = new PasswordMatchValidator();
            _passwordValidator = new PasswordValidator();
            _priceValidator = new PriceValidator();
        }

        [TestCase(4, true)]
        [TestCase(0, false)]
        [TestCase(-4, false)]
        public void _carAgeValidator_DifferentInput_NegativeAndPosetiveInput(int input, bool result)
        {
            Assert.That(_carAgeValidator.Validate(input), Is.EqualTo(result));
        }

        [TestCase("Mail@gotmail.com", true)]
        [TestCase("Mail@gotmail", true)]
        [TestCase("Mailgotmail.com", false)]
        [TestCase("", false)]
        public void _emailValidatorr_DifferentInput_NegativeAndPosetiveInput(string input, bool result)
        {
            Assert.That(_emailValidator.Validate(input), Is.EqualTo(result));
        }

        [TestCase("Mailgotmailcom", false)]
        [TestCase("123", true)]
        [TestCase("", false)]
        [TestCase("Mail@gotmail.com", false)]
        [TestCase("Mailgotmail.com", false)]
        public void _licensePlateValidator_DifferentInput_NegativeAndPosetiveInput(string input, bool result)
        {
            Assert.That(_licensePlateValidator.Validate(input), Is.EqualTo(result));
        }

        [TestCase("Mailgotmailcom", true)]
        [TestCase("123", false)]
        [TestCase("", false)]
        [TestCase("Mail@gotmail.com", true)]
        [TestCase("Mailgotmail.com", true)]
        public void _nameValidator_DifferentInput_NegativeAndPosetiveInput(string input, bool result)
        {
            Assert.That(_nameValidator.Validate(input), Is.EqualTo(result));
        }

        [TestCase("Mailgotmailcom", "Mailgotmailcom", true)]
        [TestCase("123", "123", true)]
        [TestCase("", "", true)]
        [TestCase("Mail@gotmail.com", "Mail@gotmail.com", true)]
        [TestCase("Mailgotmail.com", "Mailgotmail.com", true)]
        [TestCase("123", "213", false)]
        [TestCase("Aarhus", "aarhus", false)]
        public void _passwordMatchValidator_DifferentInput_NegativeAndPosetiveInput(string input, string otherInput, bool result)
        {
            SecureString secureinput = new SecureString();

            foreach (var Char in input)
            {
                secureinput.AppendChar(Char);
            }

            SecureString secureOtherInput = new SecureString();

            foreach (var Char in input)
            {
                secureOtherInput.AppendChar(Char);
            }
            List<SecureString> passwords = new List<SecureString>();
            passwords.Add(secureinput);
            passwords.Add(secureOtherInput);

            Assert.That(_passwordMatchValidator.Validate(passwords), Is.EqualTo(result));
        }



    }
}

