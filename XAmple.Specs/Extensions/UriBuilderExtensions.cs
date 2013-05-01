// ReSharper disable CheckNamespace

using FluentAssertions;
using NUnit.Framework;

namespace System
// ReSharper restore CheckNamespace
{
    public static class UriBuilderExtensions
    {
         public static void AddQueryString(this UriBuilder instance, string key, string value)
         {
             if (instance.Query != null && instance.Query.Length > 1)
             {
                 instance.Query = string.Format("{0}&{1}={2}", instance.Query.Substring(1), key, value);
             }
             else
             {
                 instance.Query = string.Format("{0}={1}", key, value);
             }
         }
    }

    [TestFixture]
    public class UriBuilderExtensionsTests
    {
        [Test]
        public void Test()
        {
            var expected = new Uri("http://www.example.com?param=value");
            var actual = new UriBuilder("http://www.example.com");

            actual.AddQueryString("param", "value");

            actual.Uri
                    .Should()
                    .Be(expected);
        }

        [Test]
        public void Test2()
        {
            var expected = new Uri("http://www.example.com?param1=value&param2=value");
            var actual = new UriBuilder("http://www.example.com?param1=value");

            actual.AddQueryString("param2", "value");

            actual.Uri
                    .Should()
                    .Be(expected);
        } 
    }
}