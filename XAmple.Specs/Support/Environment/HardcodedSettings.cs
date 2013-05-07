namespace XAmple.Specs.Support.Environment
{
    public class HardcodedSettings : IEnvironmentSettings
    {
        public string ApplicationBaseAddress
        {
            get { return "http://test.xample.com"; }
        }

        public string TeamCityBaseUri
        {
            get { return "http://teamcity.virjole.local"; }
        }

        public string BuildTypeId
        {
            get { return "BT3"; }
        }
    }
}