using Nancy;
using Nancy.Conventions;

namespace XAmple.Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions
                .StaticContentsConventions
                .Add(StaticContentConventionBuilder.AddDirectory("scripts"));
           
        }

    }
}