using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CarnGo;
using NSubstitute;
using NSubstitute.ClearExtensions;
using Prism.Events;

namespace CarnGo.Test.Unit
{
    [TestFixture]
    public class UserModelTests
    {
        
        [Test]
        public void DoSomeTest_TheTestisDo_DoTheTest()
        {
            //Arrange

            //Act

            //Assert
            Assert.That(true, Is.EqualTo(true));
        }


        [Test]
        public void Search_Clear_Empty()
        {
            //System.NullRefereceException. Objekt referencen er ikke sat til en instans af objekt

            //Arrange
            var clear = new SearchViewModel();

            //Act
            clear.ClearSearch();
            var testFrom = new DateTime(2019, 01, 01);
            var testTo = new DateTime(2019, 01, 01);

            var falseTestFrom = new DateTime(1995, 13, 06);
            var falseTestTo = new DateTime(1995, 13, 06);

            //Assert

            Assert.IsEmpty(clear.LocationText);
            Assert.IsEmpty(clear.BrandText);
            Assert.IsEmpty(clear.SeatsText);
            Assert.AreEqual(testFrom, clear.DateFrom);
            Assert.AreSame(testTo, clear.DateTo);
            Assert.AreNotSame(falseTestTo, clear.DateTo);
            Assert.AreNotSame(falseTestFrom, clear.DateFrom);
            
        }

        [Test]
        public void SendRequest_RentCar_ErrorTest()
        {

            //Smider string.Empty tilbage i stedet for den forventede Errortext

            //Arrange
            var request = new SendRequestViewModel(Substitute.For<IEventAggregator>());
            //Act

            var test = request.Message;
            test = "Message to leaser";
            var testError = "*Informaton was not entered correctly";

            request.To = new DateTime(1995, 06, 13);

           
            //Assert
            if (request.Message == "Message to leaser" || request.To < DateTime.Now || request.From < DateTime.Now || request.To < request.From)
            {
                Assert.AreSame(testError, request.ErrorText);
            }

        }


        [Test]
        public void Notification_Message_Isthesame()
        {
            //Arrange
            var model = new MessageModel();
            var not = new NotificationViewModel();

            //Act

            var testUser1 = new UserModel("Martin", "Gildberg", "xXxGitMazterxXx@hotmail.com", "Gellerup", UserType.Lessor);
            var testUser2 = new UserModel("Marcus", "Gasberg", "xXxGitMazterxXx@hotmail.com", "Gellerup", UserType.OrdinaryUser);
            var testCar = new CarProfileModel(testUser1, "X-360", "BMW", 1989, "1234567", "Aarhus", 2, DateTime.Today, DateTime.Today, 100);

            var testmessage1 = new MessageFromLessorModel(testUser2, testUser1, testCar, "Du kommer bare :)", true);
            var testmessage2 = new MessageFromLessorModel(testUser2, testUser1, testCar, "Det kan du godt glemme makker! Det kan du godt glemme makker! Det kan du godt glemme makker!", false);
            var testmessage3 = new MessageFromRenterModel(testUser2, testUser1, testCar, "Må jeg godt låne din flotte bil?");

      
            var item1 = new NotificationItemViewModel(testmessage1);
            var item2 = new NotificationItemViewModel(testmessage2);
            var item3 = new NotificationItemViewModel(testmessage3);

            not.Messages.Add(item1);
            not.Messages.Add(item2);
            not.Messages.Add(item3);

            Assert.IsNotEmpty(not.Messages);
            Assert.AreEqual(6,not.Messages.Count);

            not.Messages.Remove(item3);

            Assert.AreEqual(5, not.Messages.Count);

        }

       
      

        [Test]
        public void Carprofile_EditProfile_VariableIsSame()
        {
            //Arrange
            var testpcarview = new CarProfileViewModel();
            var modelacces = new CarProfileModel();

        
            //Act

            
            var testEditing = false;
            var testIsReadOnly = true;

            //Assert
            Assert.AreEqual(testEditing, testpcarview.Editing);
            Assert.AreEqual(testIsReadOnly, testpcarview.IsReadOnly);
            
         }

        [Test]
        public void Carprofile_Save_isSame()
        {
            //Arrange
            var profi = new CarProfileViewModel();
            var mode = new CarProfileModel();



            //Act
            profi.SaveFunction();

            //Assert
            Assert.AreSame(profi._editedCarProfileModel, profi._originalCarProfileModel);
            Assert.IsTrue(profi.IsReadOnly);
            Assert.IsFalse(profi.Editing);
        }

        [Test]
        public void EditUser_Edit_isNotSame()
        {
            //Arrange
            var Edit = new EditUserViewModel();
            var User = new UserModel();


            //Act

            string testFirstName = "Alparslan";
            string testLastName = "Esen";
            string testEmail = "LolMyBackBroke@Pain.dk";
            string testAddress = "Skadestuen";

            //Assert
            Assert.AreNotSame(testFirstName, Edit.FirstName);
            Assert.AreNotSame(testLastName, Edit.LastName);
            Assert.AreNotSame(testEmail, Edit.Email);
            Assert.AreNotSame(testAddress, Edit.Address);
            
        }


        

       


    }
}
