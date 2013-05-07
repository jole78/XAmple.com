namespace XAmple.Specs.Support.Environments
{
    public class HardcodedEnvironment : IEnvironment
    {
        public string ApplicationBaseAddress
        {
            get { return "http://test.xample.com"; }
        }

        public string TeamCityBaseAddress
        {
            get { return "http://teamcity.virjole.local"; }
        }

        public string BuildTypeId
        {
            get { return "BT3"; }
        }
    }
}