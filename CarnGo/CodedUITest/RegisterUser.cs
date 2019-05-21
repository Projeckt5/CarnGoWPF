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
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUITest
{
    /// <summary>
    /// Summary description for RegisterUser
    /// </summary>
    [CodedUITest]
    public class RegisterUser
    {
        public TestContext TestContext { get; set; }
        public UIMap UIMap => map ?? (map = new UIMap());
        private UIMap map;
        private string _path;
        private ApplicationUnderTest _uut;
        private IAppDbContext _dbContext;

        public RegisterUser()
        {
            _path = "../../../CarnGo/bin/Debug/CarnGo.exe";
            Assert.IsTrue(File.Exists(_path));

            _dbContext = new AppDbContext();
        }

        [TestMethod]
        public void CodedUITestMethod1()
        {
            this.UIMap.NavigateToRegisterUser();
            this.UIMap.ClickEmailBox();
            Keyboard.SendKeys("tester@tester.com");
            this.UIMap.ClickPasswordBox();
            Keyboard.SendKeys("tester1");
            this.UIMap.ClickRepeatPasswordBox();
            Keyboard.SendKeys("tester1");
            this.UIMap.CheckAgreeToAllTerms();
            this.UIMap.ClickRegister();
            _dbContext.RemoveUser("tester@tester.com");
            this.UIMap.AssertPageContainsLoginText();
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
