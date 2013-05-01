using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using XAmple.Specs.Support.Wrappers;

namespace XAmple.Specs.Steps
{
    [Binding, Scope(Tag = "env.test")]
    public class TestEnvironmentEvents
    {
        [BeforeTestRun]
        public void OnBeforeTestRun()
        {
            ApplicationApi.OnCreating = OnCreatingApplicationApi;
            TeamCityApi.OnCreating = OnCreatingTeamCityApi;
        }

        private void OnCreatingTeamCityApi(TeamCityApi instance)
        {
            instance.BaseAddress = "http://teamcity.virjole.local";
            instance.BuildTypeId = "bt3";

            /* uncomment section to use basic authentication */
            /* --- */
            //instance.OnBeforeRequest = delegate(HttpClient client)
            //                           {
            //                               client.UseBasicAuthentication("api", "pass@word1");
            //                           };
            //instance.OnCreatingGetLatestSuccessfulBuildUrl = delegate(UriBuilder builder)
            //                                                 {
            //                                                     builder.RemoveQueryString("guest");
            //                                                 };
            /* --- */ 

            
        }

        private void OnCreatingApplicationApi(ApplicationApi instance)
        {
            instance.BaseAddress = "http://test.xample.com";
        }
    }
}
