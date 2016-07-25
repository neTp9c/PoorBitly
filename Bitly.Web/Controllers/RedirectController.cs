using Bitly.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bitly.Web.Controllers
{
    public class RedirectController : Controller
    {
        private readonly ILinkManager _linkManager;

        public RedirectController(ILinkManager linkManager)
        {
            _linkManager = linkManager;
        }

        public ActionResult Index(string shortPath)
        {
            var link = _linkManager.GetWithVisitIncremention(shortPath);
            if (link == null)
            {
                return HttpNotFound();
            }

            return Redirect(link.OriginalUrl);
        }
    }
}