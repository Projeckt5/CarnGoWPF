using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using CarnGo.Database;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using System.Threading;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Message = CarnGo.Database.Models.Message;


namespace CodedUITest
{
    [CodedUITest]
    public class Udlejning
    {
        public TestContext TestContext { get; set; }
        public UIMap UIMap => map ?? (map = new UIMap());
        private UIMap map;
        private string _path;
        private ApplicationUnderTest _uut;
        private string fromDate;
        private string toDate;
        private IAppDbContext dbContext; 

        public Udlejning()
        {
            _path = "../../../CarnGo/bin/Debug/CarnGo.exe";
            Assert.IsTrue(File.Exists(_path));

            fromDate = DateTime.Today.Date.AddDays(1).ToString("dd/MM/yyyy");
            toDate = DateTime.Today.Date.AddDays(2).ToString("dd/MM/yyyy");

            dbContext = new AppDbContext();
        }

        [TestMethod]
        public void US7_AnmodningAfBiludleje_GodkendelseAfLejeAfBil()
        {
            // Arrange
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@renter");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.ClickFindCarButton();
            Thread.Sleep(2000);
            this.UIMap.ClickSearchRentCar2();
            this.UIMap.SetSendRequestFromDate();
            Keyboard.SendKeys(fromDate);
            this.UIMap.SetSendRequestToDate();
            Keyboard.SendKeys(toDate);
            Thread.Sleep(1000);
            this.UIMap.ClickSendRequestMessage();
            Keyboard.SendKeys("I would love to rent your car!");
            this.UIMap.ClickSendRequestRentCarButton();
            Thread.Sleep(1000);
            this.UIMap.ClickSignOutButton();
            Thread.Sleep(3000);

            // Act
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@owner");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.ClickTopNotification();
        }

        [TestMethod]
        public void US7_AnmodningAfBiludleje_AnmodningAfvisesAfUdlejer()
        {

            

        }

        [TestMethod]
        public void US8_UdlejerHarGodkendtAnmodningenForLejeAfBilen()
        {
            // Arrange
            //Send request
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@renter");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.ClickFindCarButton();
            Thread.Sleep(2000);
            this.UIMap.ClickSearchRentCar2();
            this.UIMap.SetSendRequestFromDate();
            Keyboard.SendKeys(fromDate);
            this.UIMap.SetSendRequestToDate();
            Keyboard.SendKeys(toDate);
            Thread.Sleep(1000);
            this.UIMap.ClickSendRequestMessage();
            Keyboard.SendKeys("I would love to rent your car!");
            this.UIMap.ClickSendRequestRentCarButton();
            Thread.Sleep(1000);
            this.UIMap.ClickSignOutButton();
            Thread.Sleep(3000);

            //Owner accepts the request 
            // Act
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@owner");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.PressNotificationButton();
            Thread.Sleep(1000);
            this.UIMap.PressConfirmOnTopNotification();
            Thread.Sleep(1000);
            this.UIMap.ClickSignOutButton();

            //Act 
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@renter");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.PressNotificationButton();
            Thread.Sleep(1000);
            this.UIMap.PressTopNotification();
            this.UIMap.ClickMessageView();
            Thread.Sleep(3000);

            //Assert
        }

        [TestMethod]
        public void US8_UdlejerAFviserAnmodningen()
        {

        }

        //Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _uut = ApplicationUnderTest.Launch(_path);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _uut.Close();
        }
    }
}
