using FluentAssertions;
using NUnit.Framework;

namespace Tests.Example.Web
{
    [TestFixture]
    public class MyTests
    {
        [Test]
        public void MyTest()
        {
            int expected = 4;

            int actual = 2 + 2;

            actual
                .Should()
                .Be(expected);
         }
    }
}
