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


        [Test]
        public void PasswordValidate_WithNumbers_Success()
        {
           
            var passsordWithNumbers = "Benbenben123";
            var ValidationWithNumbers = _uut.Validate(passsordWithNumbers.ConvertToSecureString());
                

            Assert.That(ValidationWithNumbers, Is.EqualTo(true));
        }


        [Test]
        public void PasswordValidate_WithoutNumbers_Fail()
        {
            
            var passsordWithoutNumbers = "Benbenben";

            var ValidationWithNumbers = _uut.Validate(passsordWithoutNumbers.ConvertToSecureString());




            Assert.That(ValidationWithNumbers, Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_OnlyNumbers_Fail()
        {
            
            var passsordWithOnlyNumbers = "12345678";

            var ValidationOnlyNumbers = _uut.Validate(passsordWithOnlyNumbers.ConvertToSecureString());



            Assert.That(ValidationOnlyNumbers, Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_EmptyField_fail()
        {
            
            var passwordEmpty = "";

            var ValidationEmpty = _uut.Validate(passwordEmpty.ConvertToSecureString());



            Assert.That(ValidationEmpty, Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_Length_Success()
        {
           
            var passwordTrueLength = "123BenBen";

            var ValidationTrueLength = _uut.Validate(passwordTrueLength.ConvertToSecureString());



            Assert.That(ValidationTrueLength, Is.EqualTo(true));
        }

        [Test]
        public void PasswordValidate_Length_Fail()
        {
            
            var passwordFalseLength = "12Ben";

            var ValidationFalseLength = _uut.Validate(passwordFalseLength.ConvertToSecureString());



            Assert.That(ValidationFalseLength, Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_NordicChars_Success()
        {
          
            var passwordSuccessNordicChars = "12Beøæå";

            var ValidationWithNordicChars = _uut.Validate(passwordSuccessNordicChars.ConvertToSecureString());



            Assert.That(ValidationWithNordicChars, Is.EqualTo(true));
        }
    }
}
