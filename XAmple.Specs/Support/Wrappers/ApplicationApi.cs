using System;
using EasyHttp.Http;
using Newtonsoft.Json;
using XAmple.Specs.Support.Environment;

namespace XAmple.Specs.Support.Wrappers
{
    public class ApplicationApi
    {
        private readonly IEnvironmentSettings m_Settings;

        public ApplicationApi(IEnvironmentSettings settings)
        {
            m_Settings = settings;
        }

        public Version GetVersion()
        {
            var client = new HttpClient(m_Settings.ApplicationBaseAddress)
                         {
                             Request = { Accept = HttpContentTypes.ApplicationJson }
                         };

            var response = client.Get("/about/version");
            var version = JsonConvert.DeserializeObject<Version>(response.RawText);

            return version;
        }

        public ApplicationApi WithBaseAddress(string url)
        {
            // TODO: not done thinking here
            //BaseAddress = url;
            throw new NotImplementedException();
            return this;
        }
    }
}