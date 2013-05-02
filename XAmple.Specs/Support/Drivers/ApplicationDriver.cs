using System;
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

        public Version RetrieveApplicationVersion()
        {
            var response = m_ApplicationApi.GetVersion();
            return response;
        }

    }
}