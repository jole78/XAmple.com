using TechTalk.SpecFlow;
using XAmple.Specs.Support.Drivers;

namespace XAmple.Specs.Steps
{
    [Binding]
    public class ApplicationSteps
    {
        private readonly ApplicationDriver m_Driver;

        public ApplicationSteps(ApplicationDriver driver)
        {
            m_Driver = driver;
        }

        [Given(@"I have already retrieved the application version")]
        public void RetrieveApplicationVersion_Step()
        {
            var version = m_Driver.RetrieveApplicationVersion();
            ScenarioContext.Current.Set(version, ScenarioContextKeys.ApplicationVersion); 
        }
    }
}