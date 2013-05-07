using BoDi;
using TechTalk.SpecFlow;
using XAmple.Specs.Support.Environments;

namespace XAmple.Specs.Support
{
    [Binding]
    public class EnvironmentSupport
    {
        private static IObjectContainer m_Container;

        public EnvironmentSupport(IObjectContainer container)
        {
            m_Container = container;
        }

        [BeforeScenario]
        public void InitializeEnvironment()
        {

#if DEBUG
{
    var configuration = new HardcodedEnvironment();
    m_Container.RegisterInstanceAs<IEnvironment>(configuration);
}
#endif

#if RELEASE
{
    var configuration = new ConfigurableEnvironment();
    m_Container.RegisterInstanceAs<IEnvironment>(configuration);
}
#endif

        }

    }

    
}