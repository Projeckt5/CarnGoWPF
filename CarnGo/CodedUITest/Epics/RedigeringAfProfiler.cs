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


namespace CodedUITest
{
    [CodedUITest]
    public class RedigeringAfProfiler
    {
        public TestContext TestContext { get; set; }
        public UIMap UIMap => map ?? (map = new UIMap());
        private UIMap map;
        private string _path;
        private ApplicationUnderTest _uut;
        private IAppDbContext _dbContext;

        public RedigeringAfProfiler()
        {
            _path = "../../../CarnGo/bin/Debug/CarnGo.exe";
            Assert.IsTrue(File.Exists(_path));

            _dbContext = new AppDbContext();
        }

        [TestMethod]
        public void US2_RedigeringAfBrugerProfil()
        {
            // Arrange
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@owner");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.ClickOwnerButton();

            // Act
            this.UIMap.ClickUserInformation();
            Keyboard.SendKeys("hej");
            this.UIMap.ClickUserInformationSecondRow();
            Keyboard.SendKeys("tester");
            this.UIMap.ClickUserSaveAndSignOut();
            Thread.Sleep(1000);
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@owner");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.ClickOwnerButton();

            // Assert
            this.UIMap.AssertLastNameEqualstester();
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
