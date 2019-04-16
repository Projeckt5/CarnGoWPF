using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CarnGo.Test.Unit
{
    [TestFixture]
    public class SendRequest
    {
        private SendRequestViewModel _uut;

        [SetUp]
        void setup()
        {
            _uut = new SendRequestViewModel();
        }

        [Test]
        public void RentCarFunction_MessageNoteEntered_error()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            var execute = _uut.RentCarCommand;
            Assert.That(_uut.ErrorText,Is.EqualTo("*Informaton was not entered correctly"));

        }

        
    }
}
