using System.Collections.Generic;
using Nancy.Security;

namespace XAmple.Web
{
    public class ApiUser : IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }

        public ApiUser()
        {
            UserName = "Api User";
            Claims = new string[] { "ApiUser" };
        }

    }
}