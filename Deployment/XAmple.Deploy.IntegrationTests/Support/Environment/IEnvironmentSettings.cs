using System.Collections.Generic;

namespace XAmple.Deploy.IntegrationTests.Support.Environment
{
    public interface IEnvironmentSettings
    {
        string LoadBalancedApplicationUrl { get; }
        string TeamCityBaseUrl { get; }
        string BuildTypeId { get; }
        IEnumerable<string> InternalApplicationUrls { get; }
        bool TeamCityRequiresAuthentication { get; }
        string TeamCityUserName { get; }
        string TeamCityPassword { get; }
    }
}