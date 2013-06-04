using TechTalk.SpecFlow;
using XAmple.Deploy.IntegrationTests.Support.Drivers;

namespace XAmple.Deploy.IntegrationTests.Steps
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

        [When(@"I have retrieved the deployed application version")]
        public void RetrieveDeployedApplicationVersion_Step()
        {
            m_Driver
                .RetrieveDeployedApplicationVersion();
        }

        [When(@"I have also retrieved the application version from the build server")]
        public void RetrieveBuildServersApplicationVersion_Step()
        {
            m_Driver
                .RetrieveBuildServersApplicationVersion();
        }

        [Then(@"they should all be equal")]
        public void ApplicationVersionsShouldBeEqual_Step()
        {
            m_Driver
                .ApplicationVersionsShouldBeEqual();
        }

        [Then(@"they should match")]
        public void DeployedAndBuildServerVersionsShouldMatch_Step()
        {
            m_Driver
                .DeployedAndBuildServerVersionsShouldMatch();
        }


    }
}
