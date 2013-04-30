// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    public static class UriBuilderExtensions
    {
         public static void AddQueryString(this UriBuilder instance, string key, string value)
         {
             if (instance.Query != null && instance.Query.Length > 1)
             {
                 instance.Query = string.Format("{0}&{1}={2}", instance.Query.Substring(1), key, value);
             }
             else
             {
                 instance.Query = string.Format("{0}={1}", key, value);
             }
         }
    }
}