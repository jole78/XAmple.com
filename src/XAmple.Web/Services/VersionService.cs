using System;
using System.Reflection;

namespace XAmple.Web.Services
{
    public class VersionService : IVersionService 
    {

        public Version GetVersionInformation()
        {
            // TODO: not testable
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}