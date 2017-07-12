using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Web.Http.Results;

namespace Api
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    [Route("values")]
    public class ValuesController : ApiController
    {
        //private static readonly Random _random = new Random();

        //public IEnumerable<string> Get()
        //{
        //    var random = new Random();

        //    return new[]
        //    {
        //        _random.Next(0, 10).ToString(),
        //        _random.Next(0, 10).ToString()
        //    };
        //}

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