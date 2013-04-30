using System;
using FluentAssertions;
using XAmple.Specs.Support.Wrappers;

namespace XAmple.Specs.Support.Drivers
{
    // no need for interface right now, extract when needed
    public class BuildServerDriver
    {
        private readonly TeamCityApi m_TeamCityApi;
        private Version m_Version;
        private string m_BuildTypeId;

        public BuildServerDriver(TeamCityApi teamCityApi)
        {
            m_TeamCityApi = teamCityApi;

            //uncomment to use basic authentication
            //m_TeamCityWrapper.UseBasicAuthentication("api", "pass@word1");
        }

        public void RetrieveBuildVersion()
        {
            var response = m_TeamCityApi.GetLatestSuccessfulBuild();
            var buildNO = response.Element("build").Attribute("number").Value;

            m_Version = new Version(buildNO);
        }

        // not a driver method
        public BuildServerDriver SetBuildTypeId(string id)
        {
            m_BuildTypeId = id;
            return this;
        }


    }
}