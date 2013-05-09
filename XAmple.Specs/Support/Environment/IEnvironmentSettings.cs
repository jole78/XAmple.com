using System.Collections.Generic;

namespace XAmple.Specs.Support.Environment
{
    public interface IEnvironmentSettings
    {
        string LoadBalancedApplicationUrl { get; }
        string TeamCityBaseUrl { get; }
        string BuildTypeId { get; }
        IEnumerable<string> InternalApplicationUrls { get; }
    }
}