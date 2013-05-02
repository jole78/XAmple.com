using System;
using XAmple.Specs.Support.Wrappers;

namespace XAmple.Specs.Support.Drivers
{
    public class BuildServerDriver
    {
        private readonly TeamCityApi m_TeamCityApi;

        public BuildServerDriver(TeamCityApi teamCityApi)
        {
            m_TeamCityApi = teamCityApi;
        }

        public Version RetrieveBuildVersion()
        {
            var response = m_TeamCityApi.GetRunningBuild();
            var buildNO = response.Element("build").Attribute("number").Value;
            var version = new Version(buildNO);

            return version;
        }

    }
}