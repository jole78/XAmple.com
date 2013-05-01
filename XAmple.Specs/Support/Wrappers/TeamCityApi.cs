using System;
using System.Net.Http;
using System.Xml.Linq;

namespace XAmple.Specs.Support.Wrappers
{
    public class TeamCityApi : RestApiBase
    {
        public static Action<TeamCityApi> OnCreating = delegate { };

        public string BaseAddress { get; set; }
        public string BuildTypeId { get; set; }
        public Action<HttpClient> OnBeforeRequest = delegate { };
        public Action<UriBuilder> OnCreatingGetLatestSuccessfulBuildUrl = delegate { };
         

        public TeamCityApi()
        {
            OnCreating(this);
        }

        public XElement GetLatestSuccessfulBuild()
        {
            var endpoint = new UriBuilder(string.Format("/buildTypes/id:{0}/builds?status=SUCCESS&count=1&guest=1", BuildTypeId));
            OnCreatingGetLatestSuccessfulBuildUrl(endpoint);

            XElement result = new XElement("null");
            UsingClient(delegate(HttpClient client)
                        {
                            OnBeforeRequest(client);

                            var response = client.GetAsync(endpoint.Uri).Result;
                            response.EnsureSuccessStatusCode();
                            result = response.Content.ReadAsAsync<XElement>().Result;
                        });

            return result;
        }

        protected override string GetBaseAddress()
        {
            return BaseAddress;
        }
    }
}