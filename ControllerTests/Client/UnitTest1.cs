using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenPro.WebClient.Controllers;
using System.Web.Mvc;

namespace ControllerTests.Client
{
    [TestClass]
    public class Account
    {
        [TestMethod]
        public void Test_Local_Registration_View()
        {
            AccountController account = new AccountController();
            var result = account.Register() as ViewResult;
            Assert.AreEqual("Register", result.ViewName);
        }
    }
}
