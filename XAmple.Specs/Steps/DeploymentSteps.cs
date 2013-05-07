using TechTalk.SpecFlow;
using XAmple.Specs.Support.Drivers;

namespace XAmple.Specs.Steps
{

    [Binding]
    public class DeploymentSteps
    {
        private readonly Driver m_Driver;

        public DeploymentSteps(Driver driver)
        {
            m_Driver = driver;
        }


        [Given(@"the build server's application version")]
       // [Given(@"I have knowledge of the desired application version")]
        public void RetrieveBuildVersion_Step()
        {
            m_Driver
                .RetrieveBuildVersion();
        }

        [When(@"I retrieve the current application version from (.*)")]
        public void RetreiveApplicationVersionFrom_Step(string url)
        {
            m_Driver
                .RetrieveApplicationVersionFrom(url);
        }


        [Then(@"the desired version should match the application version")]
        public void CompareVersions_Step()
        {
            m_Driver
                .ApplicationAndDesiredVersionsShouldMatch();
        }


    }
}
