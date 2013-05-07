namespace XAmple.Specs.Support.Environment
{
    public interface IEnvironmentSettings
    {
        string ApplicationBaseAddress { get; }
        string TeamCityBaseUri { get; }
        string BuildTypeId { get; }
    }
}