using System.Security.Claims;

namespace Api
{
    using System.Collections.Generic;
    using System.Web.Http;

    [Route("values")]
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            var user = User as ClaimsPrincipal;
            var nameValues = new List<string>();
            if (user == null)
            {
                nameValues.Add("Unauthorized");
            }
            else
            {
                foreach (var c in user.Claims)
                {
                    nameValues.Add($"{c.Type}: {c.Value}");
                }
            }
            return nameValues;
        }
    }
}