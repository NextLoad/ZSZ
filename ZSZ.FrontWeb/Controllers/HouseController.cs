using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IServices;

namespace ZSZ.FrontWeb.Controllers
{
    public class HouseController : Controller
    {
        public IHouse HouseService { get; set; }

        public ActionResult Index(long id)
        {
            var house = HouseService.GetById(id);
            if (house == null)
            {
                return View("~/Views/Shared/Error.cshtml", (object)"不存在的房源id");
            }
            return View(house);
        }
    }
}