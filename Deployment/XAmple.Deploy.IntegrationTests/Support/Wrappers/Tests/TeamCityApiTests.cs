using System;
using EasyHttp.Http;
using Machine.Specifications;
using Microsoft.CSharp.RuntimeBinder;
using Moq;
using XAmple.Deploy.IntegrationTests.Support.Environment;
using It = Machine.Specifications.It;

namespace XAmple.Deploy.IntegrationTests.Support.Wrappers.Tests
{
    public abstract class TeamCityApiSpecs
    {
        Establish context = () =>
        {
            EnvironmentSettings = Mock.Of<IEnvironmentSettings>();

            var mock = new Mock<TeamCityApi>(EnvironmentSettings)
                       {
                           CallBase = true
                       };
            TeamCityApi = mock.Object;
        };

        protected static TeamCityApi TeamCityApi;
        protected static IEnvironmentSettings EnvironmentSettings;
    }

    [Subject(typeof(TeamCityApi), "creating running build parameters")]
    public class when_api_uses_authentication : TeamCityApiSpecs
    {
        Establish context = () =>
        {
            Mock.Get(EnvironmentSettings)
                .Setup(x => x.TeamCityRequiresAuthentication)
                .Returns(true);
        };

        Because of = () => Parameters = TeamCityApi.CreateRunningBuildParameters();

        It should_not_contain_guest_login_parameter = () =>
        {
            Exception exception = Catch.Exception(() =>
            {
                int guest = Parameters.guest;
            });
            exception.ShouldBeOfType<RuntimeBinderException>();
        };

        static dynamic Parameters;
    }

    [Subject(typeof(TeamCityApi), "creating client")]
    public class when_api_requires_authentication : TeamCityApiSpecs
    {
        const string UserName = "user";
        const string Password = "pwd";

        Establish context = () =>
        {
            var mock = Mock.Get(EnvironmentSettings);
            mock
                .Setup(x => x.TeamCityRequiresAuthentication)
                .Returns(true);
            mock.Setup(x => x.TeamCityUserName)
                .Returns(UserName);
            mock.Setup(x => x.TeamCityPassword)
                .Returns(Password);
        };

        Because of = () => Client = TeamCityApi.CreateClient();

        It should_force_basic_authentication = () =>
        {
            Client.Request.ForceBasicAuth.ShouldBeTrue();
        };

        It should_configure_basic_authentication = () =>
        {
            Mock.Get(TeamCityApi)
                .Verify(x => x.ConfigureForBasicAuthentication(Moq.It.IsAny<HttpClient>(), UserName, Password),
                        Times.Once());
        };


        static HttpClient Client;
    }

    [Subject(typeof (TeamCityApi), "creating client")]
    public class when_api_does_not_require_authentication : TeamCityApiSpecs
    {
        Establish context = () =>
        {
            Mock.Get(EnvironmentSettings)
                .Setup(x => x.TeamCityRequiresAuthentication)
                .Returns(false);
        };

        Because of = () => { Client = TeamCityApi.CreateClient(); };

        It should_not_try_to_set_basic_authentication = () =>
        {
            Mock.Get(TeamCityApi)
                .Verify(
                    x =>
                    x.ConfigureForBasicAuthentication(Moq.It.IsAny<HttpClient>(), Moq.It.IsAny<string>(),
                                                      Moq.It.IsAny<string>()),
                    Times.Never());
        };

        It should_not_try_to_force_basic_authentication = () =>
        {
            Client.Request.ForceBasicAuth.ShouldBeFalse();
        };

        static HttpClient Client;
    }
    
}