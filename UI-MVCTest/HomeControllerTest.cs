using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SC.UI.Web.MVC.Controllers;
using System.Web.Mvc;

namespace UI_MVCTest
{
    [TestFixture]
    class HomeControllerTest
    {

        HomeController sut = new HomeController();

        [Test]
        public void IndexTest()
        {
            //Arrange
            string viewName = "Index";

            //Act
            var result = sut.Index() as ViewResult;

            //Assert
            Assert.AreEqual(viewName, result.ViewName);
        }

        [Test]
        public void AboutTest()
        {
            //Arrange
            string viewName = "About";
            string message = "Your application description page.";

            //Act
            var result = sut.About() as ViewResult;

            //
            Assert.AreEqual(viewName, result.ViewName);
            Assert.AreEqual(message, sut.ViewData["Message"]);
        }

        [Test]
        public void ContactTest()
        {
            //Arrange
            string viewName = "Contact";
            string message = "Your contact page.";

            //Act
            var result = sut.Contact() as ViewResult;

            //
            Assert.AreEqual(viewName, result.ViewName);
            Assert.AreEqual(message, sut.ViewData["Message"]);
        }
    }
}
