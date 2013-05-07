namespace XAmple.Specs.Support.Environments
{
    public interface IEnvironment
    {
        string ApplicationBaseAddress { get; }
        string TeamCityBaseAddress { get; }
        string BuildTypeId { get; }
    }
}