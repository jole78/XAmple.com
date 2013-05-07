using System.Configuration;
using System.Dynamic;

namespace XAmple.Specs.Support.Environments
{
    public class ConfigurableEnvironment : IEnvironment
    {
        private readonly dynamic m_Settings;

        public ConfigurableEnvironment()
        {
            m_Settings = new EnvironmentSettings();
        }

        public string ApplicationBaseAddress
        {
            get { return m_Settings.ApplicationUrl; }
        }

        public string TeamCityBaseAddress
        {
            get { return m_Settings.TeamCityUrl; }
        }

        public string BuildTypeId
        {
            get { return m_Settings.BuildTypeId; }
        }

        private class EnvironmentSettings : DynamicObject
        {
            private readonly Configuration m_Configuration;

            public EnvironmentSettings()
            {
                m_Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = m_Configuration.AppSettings.Settings[binder.Name];
                return true;
            }
        }
    }

    
}