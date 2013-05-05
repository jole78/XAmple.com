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
<<<<<<< HEAD
                locator = string.Format("buildType:{0},running:any", BuildTypeId),
=======
                locator = string.Format("buildType:{0},running:true", BuildTypeId),
>>>>>>> 50936e173b1bbe559093c182e93ee64f170cc439
                guest = 1,count=1
            });

            var build = response.DynamicBody.build[0];
            var buildNO = build.number;

            return new Version(buildNO);
        }


    }
}