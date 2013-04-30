using System;
using System.Net.Http;

namespace XAmple.Specs.Support.Wrappers
{
    public abstract class RestApiBase
    {
        //TODO: make it possible to use basic auth

        protected abstract string GetBaseAddress();

        protected void UsingClient(Action<HttpClient> action)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GetBaseAddress());
                action(client);
            }
        }

    }
}