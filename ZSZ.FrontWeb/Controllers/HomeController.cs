using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZSZ.FrontWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["userName"] != null)
            {
                return Content(Session["userName"].ToString());
            }

            Session["userName"] = "session";
            return Content("ok");
        }
    }
}