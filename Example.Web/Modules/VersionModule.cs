using System.Collections.Generic;
using System.Linq;
using System.Web;
using Example.Web.Services;
using Nancy;

namespace Example.Web.Modules
{
    public class VersionModule : NancyModule
    {
        private readonly IVersionService m_VersionService;

        public VersionModule(IVersionService versionService)
        {
            m_VersionService = versionService;

            Get["/"] = delegate(dynamic parameters)
                       {
                           return Response.AsJson(m_VersionService.GetVersionInformation());
                       };
        }
    }
}