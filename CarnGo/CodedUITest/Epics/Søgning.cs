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
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUITest
{
    [CodedUITest]
    public class Søgning
    {
        public TestContext TestContext { get; set; }
        public UIMap UIMap => map ?? (map = new UIMap());
        private UIMap map;
        private string _path;
        private ApplicationUnderTest _uut;

        public Søgning()
        {
            _path = "../../../CarnGo/bin/Debug/CarnGo.exe";
            Assert.IsTrue(File.Exists(_path));
        }

        [TestMethod]
        public void US6_SøgningEfterBilerTilLeje()
        {
            // Assert
            this.UIMap.ClickLoginEmailBox();
            Keyboard.SendKeys("car@renter");
            this.UIMap.ClickLoginPasswordBox();
            Keyboard.SendKeys("123asd");
            this.UIMap.ClickLoginButton();
            Thread.Sleep(1000);

            // Act
            this.UIMap.ClickFindCarButton();
            Thread.Sleep(1000);
            this.UIMap.ClickSearchCarBrandBox();
            Keyboard.SendKeys("BMW3");
            this.UIMap.ClickSearchSeatsBox();
            Keyboard.SendKeys("3");

            // Assert
            this.UIMap.ClickSearchSearchButton();
            Thread.Sleep(2000);
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
