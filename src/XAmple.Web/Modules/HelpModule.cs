using Nancy;

namespace XAmple.Web.Modules
{
    public class HelpModule : NancyModule
    {
        public HelpModule()
            :base("/help")
        {
            Get["/"] = delegate(dynamic parameters)
                       {
                           return View["Index"];
                       };
        }
    }
}