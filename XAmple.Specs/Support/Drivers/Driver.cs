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
        private List<Version> m_ApplicationVersions;
        private Version m_ApplicationVersion;

        public Driver(TeamCityApi teamCityApi, WebApplicationApi webApplicationApi, WebFarm webFarm)
        {
            m_TeamCityApi = teamCityApi;
            m_WebApplicationApi = webApplicationApi;
            m_WebFarm = webFarm;
            m_ApplicationVersions = new List<Version>();
        }

        public Driver RetrieveBuildVersion()
        {
            m_TeamCityApplicationVersion = m_TeamCityApi.GetRunningBuildVersion();
            return this;
        }

        public Driver RetrieveApplicationVersion()
        {
            m_ApplicationVersion = m_WebApplicationApi.GetVersion();
            return this;
        }

        public void ApplicationAndDesiredVersionsShouldMatch()
        {
            Assert.AreEqual(m_TeamCityApplicationVersion, m_ApplicationVersion);
        }


        public void ApplicationAndBuildVersionsShouldMatch()
        {
            Assert.AreEqual(m_TeamCityApplicationVersion, m_ApplicationVersion);
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
    }
}