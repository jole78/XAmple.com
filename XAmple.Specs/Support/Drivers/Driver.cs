using System;
using NUnit.Framework;
using XAmple.Specs.Support.Wrappers;

namespace XAmple.Specs.Support.Drivers
{
    public class Driver
    {
        private readonly TeamCityApi m_TeamCityApi;
        private readonly ApplicationApi m_ApplicationApi;

        private Version m_TeamCityApplicationVersion;
        private Version m_ApplicationVersion;

        public Driver(TeamCityApi teamCityApi, ApplicationApi applicationApi)
        {
            m_TeamCityApi = teamCityApi;
            m_ApplicationApi = applicationApi;
        }

        public Driver RetrieveBuildVersion()
        {
            m_TeamCityApplicationVersion = m_TeamCityApi.GetRunningBuildVersion();
            return this;
        }

        public Driver RetrieveApplicationVersion()
        {
            m_ApplicationVersion = m_ApplicationApi.GetVersion();
            return this;
        }

        public void ApplicationAndDesiredVersionsShouldMatch()
        {
            Assert.AreEqual(m_TeamCityApplicationVersion, m_ApplicationVersion);
        }

        public Driver RetrieveApplicationVersionFrom(string url)
        {
            m_ApplicationApi.WithBaseAddress(url);
            return RetrieveApplicationVersion();
        }

        public void ApplicationAndBuildVersionsShouldMatch()
        {
            Assert.AreEqual(m_TeamCityApplicationVersion, m_ApplicationVersion);
        }
    }
}