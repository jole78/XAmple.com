using System;
using EasyHttp.Http;
using XAmple.Deploy.IntegrationTests.Support.Environment;
using System.Dynamic;

namespace XAmple.Deploy.IntegrationTests.Support.Wrappers
{
    public class TeamCityApi
    {
        private readonly IEnvironmentSettings m_Settings;

        public TeamCityApi(IEnvironmentSettings settings)
        {
            m_Settings = settings;
        }

        public Version GetRunningBuildVersion()
        {
            var client = CreateClient();
            var parameters = CreateRunningBuildParameters();
            var response = client.Get("/httpAuth/app/rest/builds", parameters);

            var build = response.DynamicBody.build[0];
            var buildNO = build.number;

            return new Version(buildNO);
        }

        public virtual HttpClient CreateClient()
        {
            var client = new HttpClient(m_Settings.TeamCityBaseUrl)
                         {
                             Request = {Accept = HttpContentTypes.ApplicationJson}
                         };
            if (m_Settings.TeamCityRequiresAuthentication)
            {
                client.Request.ForceBasicAuth = true;
                ConfigureForBasicAuthentication(client, m_Settings.TeamCityUserName, m_Settings.TeamCityPassword);
            }

            return client;
        }

        public virtual void ConfigureForBasicAuthentication(HttpClient client, string userName, string password)
        {
            client.Request.SetBasicAuthentication(userName, password);
        }

        public virtual dynamic CreateRunningBuildParameters()
        {
            dynamic parameters = new ExpandoObject();
            if (m_Settings.TeamCityRequiresAuthentication == false)
            {
                parameters.guest = 1;
            }
            parameters.locator = string.Format("buildType:{0},running:true", m_Settings.BuildTypeId);

            // not needed any more since of EasyHttp 1.6.55.0
            //TypeDescriptor.AddProvider(new ExpandoObjectTypeDescriptionProvider(), parameters);

            return parameters;
        }
    }
}