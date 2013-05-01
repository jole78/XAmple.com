using System;
using System.Net.Http;

namespace XAmple.Specs.Support.Wrappers
{
    public abstract class RestApiBase
    {

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