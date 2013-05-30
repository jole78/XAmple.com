using System.Collections.Generic;
using XAmple.Specs.Support.Environment;

namespace XAmple.Specs.Support.Wrappers
{
    /// <summary>
    /// Accessing the web farm (servers)
    /// </summary>
    public class WebFarm
    {
        private readonly IEnvironmentSettings m_Settings;

        public WebFarm(IEnvironmentSettings settings)
        {
            m_Settings = settings;
        }

        public IEnumerable<Server> Servers
        {
            get
            {
                foreach (var url in m_Settings.InternalApplicationUrls)
                {
                    yield return new Server
                                 {
                                     ApplicationUrl = url
                                 };
                }
            }
        }

        public class Server 
        {
            public string ApplicationUrl { get; set; }
        }
    }
}