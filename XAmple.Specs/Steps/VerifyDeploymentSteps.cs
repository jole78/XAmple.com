using System;
using NUnit.Framework;
using TechTalk.SpecFlow;
using XAmple.Specs.Support.Drivers;

namespace XAmple.Specs.Steps
{

    [Binding]
    public class VerifyDeploymentSteps
    {
        private readonly ApplicationDriver m_Driver;


        public VerifyDeploymentSteps(ApplicationDriver driver)
        {
            m_Driver = driver;
        }

        [Given(@"I have retrieved the application version")]
        public void RetrieveApplicationVersion_Step()
        {
            m_Driver
                .RetrieveApplicationVersion();
        }


        [Then(@"the ""(.*)"" version and the ""(.*)"" version should match")]
        public void CompareVersions_Step(Version p1, Version p2)
        {
            Assert.AreEqual(p1, p2);
        }







    }
}
