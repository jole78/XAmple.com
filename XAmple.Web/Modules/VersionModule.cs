using Nancy;
using XAmple.Web.Services;

namespace XAmple.Web.Modules
{
    public class VersionModule : NancyModule
    {
        private readonly IVersionService m_VersionService;

        public VersionModule(IVersionService versionService)
            :base("/help/version")
        {
            m_VersionService = versionService;

            Get["/"] = delegate(dynamic parameters)
                       {
                           var version = m_VersionService.GetVersionInformation();
                           return Response.AsJson(version);
                       };
        }
    }
}