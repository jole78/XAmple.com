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

        [When(@"I have collected the application version from each of the servers in the web farm")]
        public void RetrieveApplicationVersions_Step()
        {
            m_Driver
                .RetrieveApplicationVersions();
        }

        [Then(@"they should all be equal")]
        public void ThenTheyShouldAllBeEqual_Step()
        {
            m_Driver
                .ApplicationVersionsShouldBeEqual();
        }



        [Given(@"the build server's application version")]
       // [Given(@"I have knowledge of the desired application version")]
        public void RetrieveBuildVersion_Step()
        {
            m_Driver
                .RetrieveBuildVersion();
        }



        [Then(@"the desired version should match the application version")]
        public void CompareVersions_Step()
        {
            m_Driver
                .ApplicationAndDesiredVersionsShouldMatch();
        }


    }
}
