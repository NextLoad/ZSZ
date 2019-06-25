using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
using ZSZ.AdminWeb.Models;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.AdminWeb.Controllers
{
    public class RegionController : Controller
    {
        public IRegion RegionService { get; set; }
        public ICity CityService { get; set; }
        public ActionResult List(int pageIndex = 1)
        {
            long? cityId = AdminHelper.GetCityId(this.HttpContext);
            if (cityId == null)
            {
                return View("~/Views/Shared/Error.cshtml", (object)"总部人员无法管理区域");
            }

            var regions = RegionService.GetAll(cityId.Value);
            RegionListViewModel regionListView = new RegionListViewModel();
            regionListView.Regions = regions;
            regionListView.CityId = cityId.Value;
            regionListView.PageIndex = pageIndex;
            regionListView.PageCount = 10;
            regionListView.TotalCount = regions.LongCount();
            return View(regionListView);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var cities = CityService.GetAll();
            return View(cities);
        }

        [HttpPost]
        public ActionResult Add(RegionAddViewModel regionAddView)
        {
            RegionService.AddNew(regionAddView.Name, regionAddView.CityId);
            return Json(new AjaxResult { Status = "ok" });
        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
            var region = RegionService.GetById(id);
            RegionEditGetViewModel regionEditGetView = new RegionEditGetViewModel();
            regionEditGetView.Region = region;
            regionEditGetView.Cities = CityService.GetAll();
            return View(regionEditGetView);
        }

        [HttpPost]
        public ActionResult Edit(RegionEditViewModel regionEditView)
        {
            RegionService.Update(regionEditView.Id, regionEditView.Name, regionEditView.CityId);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            RegionService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult BetchDelete(long[] ids)
        {
            foreach (long id in ids)
            {
                RegionService.MarkDeleted(id);
            }

            return Json(new AjaxResult { Status = "ok" });
        }

    }


}