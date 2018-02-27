using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace TestTutorial
{
    public class FooxUnitTest
    {
        [Fact]
        public void FooTest()
        {
            //Arrange
            Foo foo = new Foo();
            string expected = "bar";

            //Act
            string result = foo.Bar();

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("foo","bar","foobar")]
        [InlineData("boo", "far", "boofar")]
        [InlineData("", "bar", "barbar")]
        public void FooBarTest(string para1, string para2, string expected)
        {
            //Arrange
            Foo foo = new Foo();

            //Act
            string result = foo.FooBar(para1, para2);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
