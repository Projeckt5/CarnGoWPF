﻿
using System.IO;

using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;

namespace CodedUITest
{
    [CodedUITest]
    public class Typografi
    {
        public TestContext TestContext { get; set; }
        public UIMap UIMap => map ?? (map = new UIMap());
        private UIMap map;
        private string _path;
        private ApplicationUnderTest _uut;

        public Typografi()
        {
            _path = "../../../CarnGo/bin/Debug/CarnGo.exe";
            Assert.IsTrue(File.Exists(_path));
        }

        [TestMethod]
        public void Robotic_Typografi()
        {
            // Act
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@renter");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);
            this.UIMap.ClickOwnerButton();
            this.UIMap.ClickUserInformation();
            Thread.Sleep(1000);
            this.UIMap.ClickOnuserInfo();
            
            //Assert
            this.UIMap.AssertOnRoboticFont();
        }

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
