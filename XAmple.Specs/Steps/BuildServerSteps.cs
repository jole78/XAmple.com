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

<<<<<<< HEAD
=======
        [When(@"I retrieve the build version")]
        public void RetrieveBuildVersion_Step()
        {
            m_Driver
                .RetrieveBuildVersion();
        }
>>>>>>> 50936e173b1bbe559093c182e93ee64f170cc439
    }
}