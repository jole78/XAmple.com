using TechTalk.SpecFlow;
using XAmple.Specs.Support.Wrappers;

namespace XAmple.Specs.Support.Drivers
{
    public class ApplicationDriver 
    {
        private readonly ApplicationApi m_ApplicationApi;

        public ApplicationDriver(ApplicationApi applicationApi)
        {
            m_ApplicationApi = applicationApi;
        }

        public void RetrieveApplicationVersion()
        {
            var response = m_ApplicationApi.GetVersion();
            ScenarioContext.Current.Set(response, "ApplicationDriver.Version");
        }

    }
}