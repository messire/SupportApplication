using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportApplication.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Application for tickets supporting.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Some awesome message.";

            return View();
        }
    }
}