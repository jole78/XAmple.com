using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XAmple.Specs.Support.Environment
{
    public class ConfigurableSettings : IEnvironmentSettings
    {
        private readonly dynamic m_Settings;

        public ConfigurableSettings()
        {
            // TODO: a bit hardcoded perhaps
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
            get { return m_Settings.TeamCity.Url; }
        }

        public string BuildTypeId
        {
            get { return m_Settings.TeamCity.BuildTypeId; }
        }

        public IEnumerable<string> InternalApplicationUrls
        {
            get
            {
                // TODO: hardcoded to xml, is it OK??
                IEnumerable<XElement> urls = m_Settings.WebApplication.Url;
                return urls.Select(x => x.Value);

            }
        }
    }

    
}