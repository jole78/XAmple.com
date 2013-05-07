using System;
using EasyHttp.Http;
using XAmple.Specs.Support.Environments;

namespace XAmple.Specs.Support.Wrappers
{
    public class TeamCityApi
    {
        private readonly IEnvironment m_Environment;

        public TeamCityApi(IEnvironment environment)
        {
            m_Environment = environment;
        }

        public Version GetRunningBuildVersion()
        {
            //TODO: fix OnBeforeRequest and OnCreatingGetRunningBuildUrl

            var client = new HttpClient(m_Environment.TeamCityBaseAddress)
                         {
                            Request = {Accept = HttpContentTypes.ApplicationJson}
                         };
            var response = client.Get("/httpAuth/app/rest/builds", new
            {
                locator = string.Format("buildType:{0},running:true", m_Environment.BuildTypeId),
                guest = 1
            });

            var build = response.DynamicBody.build[0];
            var buildNO = build.number;

            return new Version(buildNO);
        }


    }
}