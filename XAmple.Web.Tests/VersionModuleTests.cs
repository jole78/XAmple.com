using System;
using FluentAssertions;
using FluentAutomation;
using Moq;
using NUnit.Framework;
using Nancy.Testing;
using Newtonsoft.Json;
using TinyIoC;
using XAmple.Web.Modules;
using XAmple.Web.Services;

namespace XAmple.Web.Tests
{
    [TestFixture]
    public class VersionModuleTests
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

    public class VersionModuleWrapper
    {
         
    }

    public class BrowserAutomation : FluentTest
    {
        public BrowserAutomation()
        {
            FluentAutomation.Settings.Registration = ConfigureContainer;
        }

        private void ConfigureContainer(TinyIoCContainer container)
        {
        }
    }
}
