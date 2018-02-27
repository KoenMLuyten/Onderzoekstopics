using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace TestTutorial
{
    [TestFixture]
    public class FooNUnitTest
    {
        [Test]
        public void FooTest()
        {
            //Arrange
            Foo foo = new Foo();
            string expected = "bar";

            //Act
            string result = foo.Bar();

            //Assert
            Assert.AreEqual(expected, result);
        }
        
        [TestCase("foo","bar","foobar")]
        [TestCase("bar", "foo", "barfoo")]
        [TestCase("boo", "far", "boofar")]
        [TestCase("", "bar", "barbar")]
        public void FooBarTest(string para1, string para2, string expected)
        {
            //Arrange
            Foo foo = new Foo();

            //Act
            string result = foo.FooBar(para1, para2);

            //Assert
            Assert.AreEqual(expected, result);
        }


    }
}
