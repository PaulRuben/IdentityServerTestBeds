using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Api3_DotNetFrameworkWebApi
{
    [Route("identity")]
    public class IdentityController : ApiController
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

        //public IHttpActionResult Get()
        //{
        //    var user = User as ClaimsPrincipal;
        //    if (user == null)
        //        throw new AccessViolationException();

        //    var claims = from c in user.Claims
        //        select new
        //        {
        //            type = c.Type,
        //            value = c.Value
        //        };

        //    return Json(claims);
        //}

    }
}