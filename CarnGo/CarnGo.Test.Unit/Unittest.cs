using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CarnGo;
using NSubstitute.ClearExtensions;

namespace CarnGo.Test.Unit
{
    [TestFixture]
    public class UserModelTests
    {
        [SetUp]
        public void Setup()
        {
            //Arrange
        }

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
            //Arrange
            var clear = new SearchViewModel();

            //Act
           clear.ClearSearch();
           var testFrom = new DateTime(2019, 01, 01);
           var testTo = new DateTime(2019, 01, 01);

           //Assert

            Assert.IsEmpty(clear.LocationText);
            Assert.IsEmpty(clear.BrandText);
            Assert.IsEmpty(clear.SeatsText);
            Assert.AreSame(testFrom, clear.DateFrom);
            Assert.AreSame(testTo, clear.DateTo);


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

      
            var item = new NotificationItemViewModel(testmessage2);

            not.Messages.Add(item);

            Assert.IsNotEmpty(not.Messages);
            

        }

       
      

        [Test]
        public void Carprofile_Profile_isSame()
        {
            //Arrange
            var testpcarview = new CarProfileViewModel();
            var modelacces = new CarProfileModel();
            

            //Act
            var testEditing = false;
            var testIsReadOnly = true;

            //Assert
            Assert.AreSame(testEditing, testpcarview.Editing);
            Assert.AreSame(testIsReadOnly, testpcarview.IsReadOnly);



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
            Assert.IsTrue(profi.Editing);
            Assert.IsFalse(profi.IsReadOnly);
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


        //Help Functions

       


    }
}
