using System;
using EasyHttp.Http;
using Newtonsoft.Json;
using XAmple.Specs.Support.Environments;

namespace XAmple.Specs.Support.Wrappers
{
    public class ApplicationApi
    {
        private readonly IEnvironment m_Environment;

        public ApplicationApi(IEnvironment environment)
        {
            m_Environment = environment;
        }

        public Version GetVersion()
        {
            var client = new HttpClient(m_Environment.ApplicationBaseAddress)
                         {
                             Request = { Accept = HttpContentTypes.ApplicationJson }
                         };

            var response = client.Get("/about/version");
            var version = JsonConvert.DeserializeObject<Version>(response.RawText);

            return version;
        }

        public ApplicationApi WithBaseAddress(string url)
        {
            //BaseAddress = url;
            throw new NotImplementedException();
            return this;
        }
    }
}