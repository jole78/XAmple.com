using BoDi;
using TechTalk.SpecFlow;
using XAmple.Specs.Support.Environment;

namespace XAmple.Specs.Support
{
    [Binding]
    public class EnvironmentSettingsSupport
    {
        private static IObjectContainer m_Container;

        public EnvironmentSettingsSupport(IObjectContainer container)
        {
            m_Container = container;
        }

        [BeforeScenario]
        public void InitializeEnvironment()
        {
            // settings are hardcoded
#if DEBUG
            {
                var settings = new HardcodedSettings();
                m_Container.RegisterInstanceAs<IEnvironmentSettings>(settings);
            }
#endif

            // enables the user to configure the settings via xml
#if RELEASE
            {
                var settings = new ConfigurableSettings();
                m_Container.RegisterInstanceAs<IEnvironmentSettings>(settings);
            }
#endif

        }

    }

    
}