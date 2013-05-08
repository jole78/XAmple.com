using System.Collections.Generic;

namespace XAmple.Specs.Support.Environment
{
    public class HardcodedSettings : IEnvironmentSettings
    {
        public string LoadBalancedApplicationUrl
        {
            get { return "http://test.xample.com"; }
        }

        public string TeamCityBaseUrl
        {
            get { return "http://teamcity.virjole.local"; }
        }

        public string BuildTypeId
        {
            get { return "bt3"; }
        }

        public IEnumerable<string> InternalApplicationUrls
        {
            get
            {
                return new string[]
                       {
                           "http://wfe1.test.xample.com",
                           "http://wfe2.test.xample.com"
                       };
            }
        }
    }
}