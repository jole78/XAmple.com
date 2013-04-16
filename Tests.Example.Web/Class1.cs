using System;
using Example.Web.Modules;
using Example.Web.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Nancy.Testing;
using Newtonsoft.Json;

namespace Tests.Example.Web
{
    [TestFixture]
    public class TestClass
    {
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
            var response = browser.Get("/help/version");

            // THEN
            var json = JsonConvert.DeserializeObject<Version>(response.Body.AsString());

            json.ToString().Should()
                .BeEquivalentTo("1.0.0.1");

        }
    }
}
