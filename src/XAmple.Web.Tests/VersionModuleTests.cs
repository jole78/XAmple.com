using FluentAssertions;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace XAmple.Web.Tests
{
    [TestFixture]
    public class VersionModuleTests
    {

        [Test]
        public void Get_WithoutApiKey_ShouldResultInUnauthorizedResponse()
        {
            // GIVEN
            var browser = new Browser(new Bootstrapper());

            // WHEN
            var response = browser.Get("/about/version");
            

            // THEN
            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);

        }

        [Test]
        public void Get_WithApiKey_ShouldResultInOKResponse()
        {
            // GIVEN
            var browser = new Browser(new Bootstrapper());

            // WHEN
            var response = browser.Get("/about/version", x =>
                                                         {
                                                             x.Query("ApiKey","pass@word1");
                                                         });
            // THEN
            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

        }
    }

}
