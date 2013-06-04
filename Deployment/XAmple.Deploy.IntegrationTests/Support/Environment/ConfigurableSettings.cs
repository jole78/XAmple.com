using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XAmple.Deploy.IntegrationTests.Support.Environment
{
    public class ConfigurableSettings : IEnvironmentSettings
    {
        readonly dynamic m_Settings;

        public ConfigurableSettings()
        {
            m_Settings = XElement
                .Load(@".\environment.settings.xml")
                .ToDynamic();
        }

        public string LoadBalancedApplicationUrl
        {
            get { return m_Settings.WebApplication.LoadBalancedUrl; }
        }

        public string TeamCityBaseUrl
        {
            get { return TeamCitySection.Url; }
        }

        public string BuildTypeId
        {
            get { return TeamCitySection.BuildTypeId; }
        }

        public IEnumerable<string> InternalApplicationUrls
        {
            get
            {
                IEnumerable<XElement> urls = m_Settings.WebApplication.Url;
                return urls.Select(x => x.Value);

            }
        }

        public bool TeamCityRequiresAuthentication
        {
            get { return TeamCitySection.Authenticate; }
        }

        public string TeamCityUserName
        {
            get { return TeamCitySection.User; }
        }

        public string TeamCityPassword
        {
            get { return TeamCitySection.Pwd; }
        }

        dynamic TeamCitySection
        {
            get { return m_Settings.TeamCity; }
        }
    }


}