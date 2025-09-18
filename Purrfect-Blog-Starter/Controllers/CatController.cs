using System.Web.Mvc;

namespace Purrfect_Blog_Starter.Controllers
{
    public class CatController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}