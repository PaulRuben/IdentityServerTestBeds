using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace MvcNgClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [Authorize]
        public JsonResult Identity()
        {
            var user = User as ClaimsPrincipal;
            if (user == null)
            {
                return new JsonResult {Data = "Unauthorized", JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }

            var nameValues = new List<string>();
            foreach (var c in user.Claims)
            {
                nameValues.Add($"{c.Type}: {c.Value}");
            }
            return new JsonResult {Data = nameValues, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}