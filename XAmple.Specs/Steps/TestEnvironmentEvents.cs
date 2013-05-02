using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using XAmple.Specs.Support.Wrappers;

namespace XAmple.Specs.Steps
{
    [Binding, Scope(Tag = "environment.test")]
    public class TestEnvironmentEvents
    {

        [BeforeScenario]
        public void OnBeforeScenario()
        {
            ApplicationApi.OnCreating = OnCreatingApplicationApi;
            TeamCityApi.OnCreating = OnCreatingTeamCityApi;
        }

        private static void OnCreatingTeamCityApi(TeamCityApi instance)
        {
            instance.BaseAddress = "http://teamcity.virjole.local";
            instance.BuildTypeId = "bt3"; // 1 - deploy => test

            /* uncomment section to use basic authentication */
            /* --- */
            //instance.OnBeforeRequest = delegate(HttpClient client)
            //                           {
            //                               client.UseBasicAuthentication("api", "pass@word1");
            //                           };
            //instance.OnCreatingGetRunningBuildUrl = delegate(UriBuilder builder)
            //                                        {
            //                                            builder.RemoveQueryString("guest");
            //                                        };
            /* --- */


        }

        private static void OnCreatingApplicationApi(ApplicationApi instance)
        {
            instance.BaseAddress = "http://test.xample.com";
        }
    }
}
