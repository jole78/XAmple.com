using System;
using EasyHttp.Http;
using Newtonsoft.Json;
using XAmple.Specs.Support.Environment;

namespace XAmple.Specs.Support.Wrappers
{
    /// <summary>
    /// Accessing the webapplication via its API
    /// </summary>
    public class WebApplicationApi
    {
        private string m_BaseUri;

        public WebApplicationApi(IEnvironmentSettings settings)
        {
            m_BaseUri = settings.LoadBalancedApplicationUrl;
        }

        public Version GetVersion()
        {
            var client = new HttpClient(m_BaseUri)
                         {
                             Request = { Accept = HttpContentTypes.ApplicationJson }
                         };

            var response = client.Get("/about/version",
                                      new
                                      {
                                          // TODO: should be in settings right!?
                                          apiKey="pass@word1"
                                      });
            var version = JsonConvert.DeserializeObject<Version>(response.RawText);

            return version;
        }

        public void UsingBaseAddress(string address, Action<WebApplicationApi> action)
        {
            var before = m_BaseUri;
            m_BaseUri = address;
            action(this);
            m_BaseUri = before;
        }

    }
}