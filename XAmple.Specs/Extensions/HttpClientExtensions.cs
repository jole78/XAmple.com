using System.Net.Http.Headers;
using System.Text;

// ReSharper disable CheckNamespace
namespace System.Net.Http
// ReSharper restore CheckNamespace
{
    public static class HttpClientExtensions
    {
        public static void UseBasicAuthentication(this HttpClient instance, string userName, string password)
        {
            instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                                                                                         ToBase64String(userName, password));
        }

        private static string ToBase64String(string userName, string password)
        {
            return Convert.ToBase64String(
                Encoding.ASCII
                        .GetBytes(
                            string
                                .Format
                                ("{0}:{1}",
                                 userName,
                                 password)));
        }
    }
}