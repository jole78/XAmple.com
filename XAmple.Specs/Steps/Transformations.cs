using System;
using TechTalk.SpecFlow;

namespace XAmple.Specs.Steps
{
    [Binding]
    public class Transformations
    {
        [StepArgumentTransformation]
        public Version FromString(string version)
        {
            string key = string.Format("{0}.version", version);
            Version match;
            if (ScenarioContext.Current.TryGetValue(key, out match) == false)
            {
                throw new Exception(string.Format("found no match for '{0}' in ScenarioContext", key));
            }
            return match;
        }
    }
}
