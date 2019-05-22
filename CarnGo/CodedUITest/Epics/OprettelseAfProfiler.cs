using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;
using CarnGo.Database;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUITest
{
    [CodedUITest]
    public class OprettelseAfProfiler
    {
        public TestContext TestContext { get; set; }
        public UIMap UIMap => map ?? (map = new UIMap());
        private UIMap map;
        private string _path;
        private ApplicationUnderTest _uut;
        private IAppDbContext _dbContext;

        public OprettelseAfProfiler()
        {
            _path = "../../../CarnGo/bin/Debug/CarnGo.exe";
            Assert.IsTrue(File.Exists(_path));

            _dbContext = new AppDbContext();
        }

        [TestMethod]
        public void TestDPI()
        {
            //Arrange 
            this.UIMap.OpenToFullScreen();
            this.UIMap.ClickEmailBoxTest();
            Thread.Sleep(1000);
            Keyboard.SendKeys("car@renter");
            this.UIMap.ClickOnPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.PressLogin();
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void US1_OprettelseAfBrugerProfil()
        {
            // Arrange
            this.UIMap.NavigateToRegisterUser();

            // Act
            this.UIMap.ClickEmailBox();
            Keyboard.SendKeys("tester@tester.com");
            this.UIMap.ClickPasswordBox();
            Keyboard.SendKeys("tester1");
            this.UIMap.ClickRepeatPasswordBox();
            Keyboard.SendKeys("tester1");
            this.UIMap.CheckAgreeToAllTerms();
            this.UIMap.ClickRegister();
            _dbContext.RemoveUser("tester@tester.com");

            // Assert
            this.UIMap.AssertPageContainsLoginText();
        }

        [TestMethod]
        public void US3_OprettelseAfBilProfil()
        {
            // Arrange
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@owner");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);

            // Act
            this.UIMap.ClickMyCarsButton();
            Thread.Sleep(1000);
            this.UIMap.ClickYourCarsEditButton();
            Thread.Sleep(1000);
            this.UIMap.ClickYourCarsCarMake();
            Keyboard.SendKeys("Audi");
            this.UIMap.ClickYourCarsCarModel();
            Keyboard.SendKeys("A6");
            this.UIMap.ClickYourCarsRegNr();
            Keyboard.SendKeys("CG56101");
            Thread.Sleep(1000);
            this.UIMap.ClickYourCarsSaveButton();
            this.UIMap.ClickSignOutButton();
            Thread.Sleep(1000);
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@owner");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.ClickMyCarsButton();

            // Assert - Can't assert properly, but test should fail
            Thread.Sleep(2000);
            Assert.IsTrue(false);
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
