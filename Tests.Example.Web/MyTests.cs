using Xunit;
using FluentAssertions;

namespace Tests.Example.Web
{
    public class MyTests
    {
        [Fact]
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
