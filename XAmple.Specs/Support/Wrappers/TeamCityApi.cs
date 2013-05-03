using System;
using EasyHttp.Http;

namespace XAmple.Specs.Support.Wrappers
{
    public class TeamCityApi
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

        public Version GetRunningBuildVersion()
        {
            //TODO: fix OnBeforeRequest and OnCreatingGetRunningBuildUrl

            var client = new HttpClient(BaseAddress)
                         {
                            Request = {Accept = HttpContentTypes.ApplicationJson}
                         };
            var response = client.Get("/httpAuth/app/rest/builds", new
            {
                locator = string.Format("buildType:{0},running:true", BuildTypeId),
                guest = 1
            });

            var build = response.DynamicBody.build;
            var buildNO = build.number;

            return new Version(buildNO);
        }


    }
}