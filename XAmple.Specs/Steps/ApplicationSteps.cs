using TechTalk.SpecFlow;
using XAmple.Specs.Support.Drivers;

namespace XAmple.Specs.Steps
{
    [Binding]
    public class ApplicationSteps
    {
        private readonly Driver m_Driver;

        public ApplicationSteps(Driver driver)
        {
            m_Driver = driver;
        }

        [Given(@"I have already retrieved the application version")]
        public void RetrieveApplicationVersion_Step()
        {
            m_Driver
                .RetrieveApplicationVersion();
        }
    }
}