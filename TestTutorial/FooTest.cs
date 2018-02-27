using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTutorial
{
    [TestClass]
    public class FooTest
    {
        [TestMethod]
        public void BarTest()
        {
            //Arrange
            Foo foo = new Foo();
            string expected = "bar";
            
            //Act
            string result = foo.Bar();
            
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FooBarTest()
        {
            //Arrange
            Foo foo = new Foo();
            string para1 = "foo";
            string para2 = "bar";
            string expected = "foobar";

            //Act
            string result = foo.FooBar(para1, para2);

            //Assert
            Assert.AreEqual(expected, result);

            
        }
    }
}
