using System;
using NUnit.Framework;
using TechTalk.SpecFlow;
using XAmple.Specs.Support.Drivers;

namespace XAmple.Specs.Steps
{

    [Binding]
    public class VerifyDeploymentSteps
    {
        private readonly Driver m_Driver;

        public VerifyDeploymentSteps(Driver driver)
        {
            m_Driver = driver;
        }

        [Then(@"the application version and the build version should match")]
        public void CompareVersions_Step()
        {
            m_Driver
                .ApplicationAndBuildVersionsShouldMatch();
        }

    }
}
