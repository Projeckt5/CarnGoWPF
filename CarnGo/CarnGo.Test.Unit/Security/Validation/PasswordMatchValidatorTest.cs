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
    public class PasswordMatchValidatorTest
    {
        private PasswordMatchValidator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new PasswordMatchValidator();


        }
        [Test]
        public void PasswordMatch_PasswordMatching_Success()
        {
            
            var passwordMatch = "12Beøæå";
            var SecurePassword = passwordMatch.ConvertToSecureString();

            List<SecureString> mylist = new List<SecureString> { SecurePassword, SecurePassword };

            bool ValidationMatchTrue = _uut.Validate(mylist);


            Assert.That(ValidationMatchTrue, Is.EqualTo(true));
        }

        [Test]
        public void PasswordMatch_PasswordMatching_Fail()
        {
            
            var passwordMatch = "12Beøæå";
            var passwordMatch2 = "12Beøæå2";

            var SecurePassword = passwordMatch.ConvertToSecureString();
            var SecurePassword2 = passwordMatch2.ConvertToSecureString();

            List<SecureString> mylist = new List<SecureString> { SecurePassword, SecurePassword2 };

            bool ValidationMatchFalse = _uut.Validate(mylist);


            Assert.That(ValidationMatchFalse, Is.EqualTo(false));
        }




    }
}
