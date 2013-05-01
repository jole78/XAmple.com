using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace XAmple.Specs.Steps
{

    [Binding]
    public class VerifyDeploymentSteps
    {

        [Then(@"the application version and the build version should match")]
        public void CompareVersions_Step()
        {
            var p1 = ScenarioContext.Current.Get<Version>(ScenarioContextKeys.ApplicationVersion);
            var p2 = ScenarioContext.Current.Get<Version>(ScenarioContextKeys.BuildVersion);

            Assert.AreEqual(p1, p2);
        }

    }
}
