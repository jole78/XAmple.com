// ReSharper disable CheckNamespace

using System.Web;
using FluentAssertions;
using NUnit.Framework;

namespace System
// ReSharper restore CheckNamespace
{
    public static class UriBuilderExtensions
    {
         public static void AddQueryString(this UriBuilder instance, string key, string value)
         {
             var query = HttpUtility.ParseQueryString(instance.Query);
             query.Set(key, value);

             instance.Query = query.ToString();
         }

        public static void RemoveQueryString(this UriBuilder instance, string key)
        {
            var query = HttpUtility.ParseQueryString(instance.Query);
            query.Remove(key);

            instance.Query = query.ToString();
        }
    }

    [TestFixture]
    public class UriBuilderExtensionsTests
    {
        [Test]
        public void RemoveQueryStringTest()
        {
            var expected = new Uri("http://www.example.com");
            var actual = new UriBuilder("http://www.example.com?param=value");

            actual.RemoveQueryString("param");

            actual.Uri
                  .Should()
                  .Be(expected);
        }

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