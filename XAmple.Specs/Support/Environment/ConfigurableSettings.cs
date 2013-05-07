using System.Xml.Linq;

namespace XAmple.Specs.Support.Environment
{
    public class ConfigurableSettings : IEnvironmentSettings
    {
        private readonly dynamic m_Settings;

        public ConfigurableSettings()
        {
            m_Settings = XElement.Load(@".\environment.settings.xml").ToDynamic();
        }

        public string ApplicationBaseAddress
        {
            get { return m_Settings.WebApplication.Url; }
        }

        public string TeamCityBaseUri
        {
            get { return m_Settings.TeamCity.Url; }
        }

        public string BuildTypeId
        {
            get { return m_Settings.TeamCity.BuildTypeId; }
        }

    }

    
}