using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace XAmple.Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            var configuration =
                new StatelessAuthenticationConfiguration(
                    delegate(NancyContext ctx)
                    {
                        var apiKey = (string)ctx.Request.Query.ApiKey.Value;

                        if (apiKey.Equals("pass@word1"))
                        {
                            return new ApiUser();
                        }
                        return null;
                    });

            AllowAccessToConsumingSite(pipelines);

            StatelessAuthentication.Enable(pipelines, configuration);
        }

        static void AllowAccessToConsumingSite(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(x =>
            {
                x.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                x.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS");
            });
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions
                .StaticContentsConventions
                .Add(StaticContentConventionBuilder.AddDirectory("scripts"));
           
        }

    }
}