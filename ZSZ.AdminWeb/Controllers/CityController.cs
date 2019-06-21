using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.AdminWeb.Controllers
{
    public class CityController : Controller
    {
        public ICity CityService { get; set; }
        public ActionResult List(int pageIndex = 1)
        {
            var allCities = CityService.GetPageData(pageIndex, 10);
            long citiesCount = CityService.GetAll().LongCount();
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = citiesCount;
            return View(allCities);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string name)
        {
            CityService.AddNewCity(name);
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var city = CityService.GetById(id);
            return View(city);
        }

        [HttpPost]
        public ActionResult Edit(long id,string name)
        {
            CityService.UpdateCity(id, name);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            CityService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult BetchDelete(long[] ids)
        {
            foreach (long id in ids)
            {
                CityService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }

    }
}