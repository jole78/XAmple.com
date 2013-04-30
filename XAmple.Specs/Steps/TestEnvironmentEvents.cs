using System;
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
        }

        private void OnCreatingApplicationApi(ApplicationApi instance)
        {
            instance.BaseAddress = "http://test.xample.com";
        }
    }
}
