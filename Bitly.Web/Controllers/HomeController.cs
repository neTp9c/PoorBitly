using Bitly.Business;
using System.Web.Mvc;

namespace Bitly.Web.Controllers
{
    public class HomeController : Controller
    {
        private ILinkManager _linkManager;

        public HomeController(ILinkManager linkManager)
        {
            _linkManager = linkManager;
        }

        public ActionResult Index()
        {
            var links = _linkManager.GetLinks(new long[] { 1, 2, 3, 4, 5 });
            return View(links);
        }
    }
}