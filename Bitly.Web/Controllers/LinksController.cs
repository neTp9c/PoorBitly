﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bitly.Web.Controllers
{
	public class LinksController : Controller
	{
        public ActionResult Index()
        {
            return View();
        }
	}
}