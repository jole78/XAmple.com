using System;
using System.Collections.Generic;
using System.Net.Http;

namespace XAmple.Specs.Support.Wrappers
{
    public class ApplicationApi : RestApiBase
    {
        public static Action<ApplicationApi> OnCreating = delegate { };

        public string BaseAddress { get; set; }

        public ApplicationApi()
        {
            OnCreating(this);
        }

        public Version GetVersion()
        {
            var endpoint = new Uri("/about/version", UriKind.Relative);

            Version result = null;
            UsingClient(delegate(HttpClient client)
                        {
                            var response = client.GetAsync(endpoint).Result;
                            response.EnsureSuccessStatusCode();

                            result = response.Content.ReadAsAsync<Version>().Result;
                        });

            return result;
        }

        protected override string GetBaseAddress()
        {
            return BaseAddress;
        }
    }
}