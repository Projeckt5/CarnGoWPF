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
    public class EmailValidatorTests
    {

        private EmailValidator _uut;
        private UserModel _fakeUserModel;


        [SetUp]
        public void Setup()
        {
            _fakeUserModel = new UserModel
            {
                Firstname = "Marcus",
                Lastname = "Jensen",
                Email = "benben23@gmail.com",
                Address = null,


            };

             _uut = new EmailValidator();

        }

        [Test]
        public void EmailValidate_Structure_Success()
        {

            bool EmailValidationTrue = _uut.Validate(_fakeUserModel.Email);
            
            Assert.That((EmailValidationTrue), Is.EqualTo(true));


        }

        [Test]
        public void EmailValidate_Structure_Fail()
        {
            bool EmailValidationFalse = _uut.Validate(_fakeUserModel.Firstname);

            Assert.That((EmailValidationFalse), Is.EqualTo(false));


        }
    }
}
