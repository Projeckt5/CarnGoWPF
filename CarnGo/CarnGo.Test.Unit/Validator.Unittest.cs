﻿using System;
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
        private IValidator<List<SecureString>> _fakePasswordMatchValidator;


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

        [Test]
        public void PasswordValidate_OnlyNumbers_Fail()
        {
            var uut = new PasswordValidator();
            var passsordWithOnlyNumbers = "12345678";

            var SecurePassword = SecureStringHelpers.ConvertToSecureString(passsordWithOnlyNumbers);



            Assert.That(uut.Validate(SecurePassword), Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_EmptyField_fail()
        {
            var uut = new PasswordValidator();
            var passwordEmpty = "";

            var SecurePassword = SecureStringHelpers.ConvertToSecureString(passwordEmpty);



            Assert.That(uut.Validate(SecurePassword), Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_Length_Success()
        {
            var uut = new PasswordValidator();
            var passwordTrueLength = "123BenBen";

            var SecurePassword = SecureStringHelpers.ConvertToSecureString(passwordTrueLength);



            Assert.That(uut.Validate(SecurePassword), Is.EqualTo(true));
        }

        [Test]
        public void PasswordValidate_Length_Fail()
        {
            var uut = new PasswordValidator();
            var passwordFailLength = "12Ben";

            var SecurePassword = SecureStringHelpers.ConvertToSecureString(passwordFailLength);



            Assert.That(uut.Validate(SecurePassword), Is.EqualTo(false));
        }

        [Test]
        public void PasswordValidate_NordicChars_Success()
        {
            var uut = new PasswordValidator();
            var passwordSuccessNordicChars = "12Beøæå";

            var SecurePassword = SecureStringHelpers.ConvertToSecureString(passwordSuccessNordicChars);



            Assert.That(uut.Validate(SecurePassword), Is.EqualTo(true));
        }

        [Test]
        public void PasswordMatch_PasswordMatching_Success()
        {
            var uut = new PasswordMatchValidator();
            var passwordMatch = "12Beøæå";

            var SecurePassword = SecureStringHelpers.ConvertToSecureString(passwordMatch);

           

            List<SecureString> mylist = new List<SecureString>{ SecurePassword, SecurePassword};
            

            
           
           
            


            Assert.That(uut.Validate(mylist), Is.EqualTo(true));
        }

        [Test]
        public void PasswordMatch_PasswordMatching_Fail()
        {
            var uut = new PasswordMatchValidator();
            var passwordMatch = "12Beøæå";
            var passwordMatch2 = "12Beøæå2";

            var SecurePassword = SecureStringHelpers.ConvertToSecureString(passwordMatch);
            var SecurePassword2 = SecureStringHelpers.ConvertToSecureString(passwordMatch2);


            List<SecureString> mylist = new List<SecureString> { SecurePassword, SecurePassword2};
       

            Assert.That(uut.Validate(mylist), Is.EqualTo(false));
        }

    }
}