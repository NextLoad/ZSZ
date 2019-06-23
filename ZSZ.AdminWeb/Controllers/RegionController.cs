using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.Models;
using ZSZ.IServices;

namespace ZSZ.AdminWeb.Controllers
{
    public class RegionController : Controller
    {
        public IRegion RegionService { get; set; }
        public ActionResult List(long cityId=1,int pageIndex=1)
        {
            var regions = RegionService.GetAll(cityId);
            RegionListViewModel regionListView = new RegionListViewModel();
            regionListView.Regions = regions;
            regionListView.CityId = cityId;
            regionListView.PageIndex = pageIndex;
            regionListView.TotalCount = regions.LongCount();
            return View(regionListView);
        }
    }
}