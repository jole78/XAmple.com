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
        public Action<UriBuilder> OnCreatingGetRunningBuildUrl = delegate { };
         

        public TeamCityApi()
        {
            OnCreating(this);
        }

        public XElement GetRunningBuild()
        {
            var endpoint = new UriBuilder
                           {
                               Path = string.Format("/httpAuth/app/rest/buildTypes/id:{0}/builds", BuildTypeId),
                               Query = "locator=running:true&guest=1"
                           };
            OnCreatingGetRunningBuildUrl(endpoint);

            XElement result = new XElement("null");
            UsingClient(delegate(HttpClient client)
                        {
                            OnBeforeRequest(client);

                            var response = client.GetAsync(endpoint.Uri.PathAndQuery).Result;
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