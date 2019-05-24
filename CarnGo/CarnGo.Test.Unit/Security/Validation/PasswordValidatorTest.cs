using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prism.Events;
using System.Drawing;
using System.Security;
using CarnGo.Security;
using NSubstitute;
using NSubstitute.Extensions;

namespace CarnGo.Test.Unit
{
    [TestFixture]
    public class PasswordValidatorTest
    {
        private PasswordValidator _uut;

        [SetUp]
        public void Setup()
        {
          _uut = new PasswordValidator();
        }


        [TestCase("Ben123")]
        [TestCase("Ben1234")]
        public void PasswordValidate_WithNumbers_Success(string password)
        {
            var passsordWithNumbers = password.ConvertToSecureString();

            var result = _uut.Validate(passsordWithNumbers);
                

            Assert.That(result, Is.EqualTo(true));
        }


        [TestCase("Benbenb")]
        [TestCase("Benben")]
        [TestCase("Benbe")]
        public void PasswordValidate_WithoutNumbers_Fail(string password)
        {
            
            var passsordWithoutNumbers = password.ConvertToSecureString();

            var result = _uut.Validate(passsordWithoutNumbers);


            Assert.That(result, Is.EqualTo(false));
        }

        [TestCase("1234567")]
        [TestCase("123456")]
        [TestCase("12345")]
        public void PasswordValidate_OnlyNumbers_Fail(string password)
        {
            
            var passsordWithOnlyNumbers = password.ConvertToSecureString();

            var result = _uut.Validate(passsordWithOnlyNumbers);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_EmptyField_fail()
        {
            
            var passwordEmpty = "".ConvertToSecureString();

            var result = _uut.Validate(passwordEmpty);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_Length_Success()
        {
           
            var passwordTrueLength = "123BenBen".ConvertToSecureString();

            var ValidationTrueLength = _uut.Validate(passwordTrueLength);

            Assert.That(ValidationTrueLength, Is.EqualTo(true));
        }

        [Test]
        public void PasswordValidate_Length_Fail()
        {
            var passwordFalseLength = "12Ben".ConvertToSecureString();

            var result = _uut.Validate(passwordFalseLength);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_NordicChars_Success()
        {
          
            var passwordSuccessNordicChars = "12Beøæå".ConvertToSecureString();

            var result = _uut.Validate(passwordSuccessNordicChars);


            Assert.That(result, Is.EqualTo(true));
        }
    }
}
