using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Api3_DotNetFrameworkWebApi.Controllers
{
    [Route("identity")]
    public class IdentityController : ApiController
    {
        public IHttpActionResult Get()
        {
            var user = User as ClaimsPrincipal;
            if (user == null)
                throw new AccessViolationException();

            var claims = from c in user.Claims
                select new
                {
                    type = c.Type,
                    value = c.Value
                };

            return Json(claims);
        }
    }
}