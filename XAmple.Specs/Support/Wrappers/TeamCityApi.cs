using System;
using System.Net.Http;
using System.Xml.Linq;

namespace XAmple.Specs.Support.Wrappers
{
    public class TeamCityApi : RestApiBase
    {
        private bool m_Authenticated = false;

        public static Action<TeamCityApi> OnCreating = delegate { };

        public string BaseAddress { get; set; }
        public string BuildTypeId { get; set; }

        public TeamCityApi()
        {
            OnCreating(this);
        }

        public XElement GetLatestSuccessfulBuild()
        {
            var endpoint = new UriBuilder(string.Format("/buildTypes/id:{0}/builds?status=SUCCESS&count=1", BuildTypeId));

            if (m_Authenticated == false)
            {
                //&guest=1
                endpoint.AddQueryString("guest", "1");
            }

            XElement result = new XElement("null");
            UsingClient(delegate(HttpClient client)
                {
                    var response = client.GetAsync(endpoint.Uri).Result;
                    response.EnsureSuccessStatusCode();
                    result = response.Content.ReadAsAsync<XElement>().Result;
                });

            return result;
        }

        public void UseBasicAuthentication(string user, string password)
        {
            m_Authenticated = true;
            throw new System.NotImplementedException();
        }

        protected override string GetBaseAddress()
        {
            return BaseAddress;
        }
    }
}