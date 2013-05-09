using System;
using EasyHttp.Http;
using XAmple.Specs.Support.Environment;

namespace XAmple.Specs.Support.Wrappers
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
            //TODO: add authentication (remove guest=1)
            //TODO: add basic auth (user + pass)

            var client = new HttpClient(m_Settings.TeamCityBaseUrl)
                         {
                            Request = {Accept = HttpContentTypes.ApplicationJson}
                         };
            var response = client.Get("/httpAuth/app/rest/builds", new
            {
                locator = string.Format("buildType:{0},running:true", m_Settings.BuildTypeId),
                guest = 1
            });

            var build = response.DynamicBody.build[0];
            var buildNO = build.number;

            return new Version(buildNO);
        }


    }
}