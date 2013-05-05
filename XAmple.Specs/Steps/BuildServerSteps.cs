using TechTalk.SpecFlow;
using XAmple.Specs.Support.Drivers;

namespace XAmple.Specs.Steps
{

    [Binding]
    public class BuildServerSteps
    {
        private readonly Driver m_Driver;

        public BuildServerSteps(Driver driver)
        {
            m_Driver = driver;
        }

    }
}