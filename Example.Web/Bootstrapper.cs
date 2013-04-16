using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Serialization.JsonNet;

namespace Example.Web
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

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(configuration =>
                                                                {
                                                                    configuration.Serializers.Insert(0, typeof(JsonNetSerializer));
                                                                });
            }
        }
    }
}