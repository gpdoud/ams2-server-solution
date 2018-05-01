using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ams2.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			ViewBag.Title = "AMS Api";

			return View();
		}
	}
}
