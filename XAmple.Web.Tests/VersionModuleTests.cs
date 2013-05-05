using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Nancy;
using Nancy.Security;
using Nancy.Testing;
using XAmple.Web.Modules;
using XAmple.Web.Services;

namespace XAmple.Web.Tests
{
    [TestFixture]
    public class VersionModuleTests
    {
        // TODO: investigate why ApiUser object causes error

        [Test]
        public void TEST()
        {
            // GIVEN
            var versionService = Mock.Of<IVersionService>();

            var browser = new Browser(cfg =>
                                      {
                                          cfg.Module<VersionModule>();
                                          cfg.Dependency<IVersionService>(versionService);
                                      });

            Mock.Get(versionService)
                .Setup(x => x.GetVersionInformation())
                .Returns(new Version("1.0.0.1"));

            // WHEN
            var response = browser.Get("/about/version");
            

            // THEN
            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);

        }

        [Test]
        public void TEST2()
        {
            // GIVEN
            var versionService = Mock.Of<IVersionService>();

            var browser = new Browser(cfg =>
            {
                cfg.Module<VersionModule>();
                cfg.Dependency<IVersionService>(versionService);
            });

            Mock.Get(versionService)
                .Setup(x => x.GetVersionInformation())
                .Returns(new Version("1.0.0.1"));

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
