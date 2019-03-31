//using System.Security;
//using CarnGo.Security;
//using NUnit.Framework;

//namespace CarnGo.Test.Unit.Security
//{
//    [TestFixture]
//    public class SecureStringHelpersTest
//    {
//        [TestCase("p4ssw0rd")]
//        [TestCase("1234")]
//        [TestCase("@asd234")]
//        [TestCase("")]
//        public void ConvertToSecureString_SecureStringContainsPassword_PasswordIsRight(string password)
//        {
//            var securePassword = password.ConvertToSecureString();

//            var result = new System.Net.NetworkCredential(string.Empty, securePassword).Password;

//            Assert.That(result,Is.EqualTo(password));
//        }

//        [TestCase("p4ssw0rd")]
//        [TestCase("1234")]
//        [TestCase("@asd234")]
//        [TestCase("")]
//        public void ConvertToString_StringContainsPassword_PasswordIsRight(string password)
//        {
//            var secureString = new SecureString();
//            foreach (var c in password)
//            {
//                secureString.AppendChar(c);
//            }
//            secureString.MakeReadOnly();

//            var result = secureString.ConvertToString();

//            Assert.That(result,Is.EqualTo(password));
//        }
//    }
//}