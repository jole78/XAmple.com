using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using XAmple.Specs.Support.Wrappers;

namespace XAmple.Specs.Support.Drivers
{
    public class Driver
    {
        private readonly TeamCityApi m_TeamCityApi;
        private readonly WebApplicationApi m_WebApplicationApi;
        private readonly WebFarm m_WebFarm;

        private Version m_TeamCityApplicationVersion;
        private readonly List<Version> m_ApplicationVersions;
        private Version m_DeployedApplicationVersion;

        public Driver(TeamCityApi teamCityApi, WebApplicationApi webApplicationApi, WebFarm webFarm)
        {
            m_TeamCityApi = teamCityApi;
            m_WebApplicationApi = webApplicationApi;
            m_WebFarm = webFarm;
            m_ApplicationVersions = new List<Version>();
        }

        public Driver RetrieveBuildServersApplicationVersion()
        {
            m_TeamCityApplicationVersion = m_TeamCityApi.GetRunningBuildVersion();
            return this;
        }

        public void DeployedAndBuildServerVersionsShouldMatch()
        {
            Assert.AreEqual(m_TeamCityApplicationVersion, m_DeployedApplicationVersion);
        }


        public void ApplicationAndBuildVersionsShouldMatch()
        {
            Assert.AreEqual(m_TeamCityApplicationVersion, m_DeployedApplicationVersion);
        }


        public Driver RetrieveApplicationVersions()
        {
            foreach (var server in m_WebFarm.Servers)
            {
                m_WebApplicationApi
                    .UsingBaseAddress(server.ApplicationUrl,
                                      x =>
                                      {
                                          var version = x.GetVersion();

                                          if (m_ApplicationVersions.Any(v => v == version) == false)
                                          {
                                              m_ApplicationVersions.Add(version);
                                          }
                                      });
            }

            return this;
        }

        public Driver ApplicationVersionsShouldBeEqual()
        {
            m_ApplicationVersions
                .Should()
                .HaveCount(1);

            return this;
        }

        public Driver RetrieveDeployedApplicationVersion()
        {
            m_DeployedApplicationVersion = m_WebApplicationApi.GetVersion();
            return this;
        }
    }
}