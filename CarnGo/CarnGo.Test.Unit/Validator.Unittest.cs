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
    public class Validatortests
    {
        
        private UserModel _uutUserModel;
       
     
        [SetUp]
        public void Setup()
        {
            _uutUserModel = new UserModel
            {
                Firstname = "Marcus",
                Lastname = "Jensen",
                Email = "benben23@gmail.com",
                Address = null,
                

            };

            

        }

        [Test]
        public void EmailValidate_Structure_Success()
        {
            var uut = new EmailValidator();

            Assert.That(uut.Validate(_uutUserModel.Email), Is.EqualTo(true));

            
        }

        [Test]
        public void EmailValidate_Structure_Fail()
        {
            var uut = new EmailValidator();

            Assert.That(uut.Validate(_uutUserModel.Lastname), Is.EqualTo(false));


        }

        [Test]
        public void PasswordValidate_WithNumbers_Success()
        {
            var uut = new PasswordValidator();
            var passsordWithNumbers = "Benbenben123";

            var SecurePassword = SecureStringHelpers.ConvertToSecureString(passsordWithNumbers);



            Assert.That(uut.Validate(SecurePassword), Is.EqualTo(true));
        }


        [Test]
        public void PasswordValidate_WithoutNumbers_Fail()
        {
            var uut = new PasswordValidator();
            var passsordWithoutNumbers = "Benbenben";

            var SecurePassword =  SecureStringHelpers.ConvertToSecureString(passsordWithoutNumbers);
            


            Assert.That(uut.Validate(SecurePassword), Is.EqualTo(false));
        }
       

    }
}
