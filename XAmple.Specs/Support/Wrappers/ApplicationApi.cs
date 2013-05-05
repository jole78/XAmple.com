using System;
using EasyHttp.Http;
using Newtonsoft.Json;

namespace XAmple.Specs.Support.Wrappers
{
    public class ApplicationApi
    {
        public static Action<ApplicationApi> OnCreating = delegate { };

        public string BaseAddress { get; set; }

        public ApplicationApi()
        {
            OnCreating(this);
        }

        public Version GetVersion()
        {
            var client = new HttpClient(BaseAddress)
                         {
                             Request = { Accept = HttpContentTypes.ApplicationJson }
                         };

            var response = client.Get("/about/version");
            var version = JsonConvert.DeserializeObject<Version>(response.RawText);

            return version;
        }

        public ApplicationApi WithBaseAddress(string url)
        {
            BaseAddress = url;
            return this;
        }
    }
}