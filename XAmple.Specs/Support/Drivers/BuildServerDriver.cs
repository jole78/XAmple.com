using System;
using TechTalk.SpecFlow;
using XAmple.Specs.Support.Wrappers;

namespace XAmple.Specs.Support.Drivers
{
    public class BuildServerDriver
    {
        private readonly TeamCityApi m_TeamCityApi;

        public BuildServerDriver(TeamCityApi teamCityApi)
        {
            m_TeamCityApi = teamCityApi;

            //uncomment to use basic authentication
            //m_TeamCityWrapper.UseBasicAuthentication("api", "pass@word1");
        }

        public Version RetrieveBuildVersion()
        {
            var response = m_TeamCityApi.GetLatestSuccessfulBuild();
            var buildNO = response.Element("build").Attribute("number").Value;
            var version = new Version(buildNO);

            return version;
        }

    }
}