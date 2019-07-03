using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
using ZSZ.AdminWeb.Models;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.AdminWeb.Controllers
{
    public class CommunitityController : Controller
    {
        public ICommunitity CommunitityService { get; set; }
        public IRegion RegionService { get; set; }

        //[CheckPermission("Communitity.list")]
        public ActionResult List(CommunitityListModel communitityList)
        {
            //var communitities = CommunitityService.GetByRegionId(communitityList.RegionId);
            long? cityId = AdminHelper.GetCityId(this.HttpContext);
            if (cityId == null)
            {
                return View("~/Views/Shared/Error.cshtml", (object)"总部人员无法管理区域");
            }
            var communitities = CommunitityService.GetByCityId(cityId.Value);
            if (communitityList.StartDateTime != DateTime.MinValue)
            {
                communitities = communitities.Where(a => a.BuiltYear >= communitityList.StartDateTime.Year).ToArray();
            }

            if (communitityList.EndDateTime != DateTime.MinValue)
            {
                communitities = communitities.Where(a => a.BuiltYear <= communitityList.StartDateTime.Year).ToArray();
            }

            if (!string.IsNullOrEmpty(communitityList.Name))
            {
                communitities = communitities.Where(a => a.Name.ToLowerInvariant().Contains(communitityList.Name.ToLowerInvariant())).ToArray();
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new AjaxResult
                {
                    Status = "ok",
                    Data = Request.Url.AbsoluteUri
                });
            }

            CommunitityListViewModel communitityListView = new CommunitityListViewModel();
            communitityListView.Communitities = communitities;
            communitityListView.RegionId = communitityList.RegionId;
            communitityListView.PageCount = communitityList.PageCount;
            communitityListView.PageIndex = communitityList.PageIndex;
            communitityListView.TotalCount = communitities.LongCount();
            return View(communitityListView);
        }

        [HttpGet]
        public ActionResult Add()
        {
            long? userId = AdminHelper.GetUserId(this.HttpContext);
            if (userId == null)
            {
                return Redirect("~/Main/Login");
            }
            long? cityId = AdminHelper.GetCityId(this.HttpContext);
            if (cityId == null)
            {
                return View("~/Views/Shared/Error.cshtml", (object)"总部人员无法管理小区！");
            }

            var regions = RegionService.GetAll(cityId.Value);
            return View(regions);
        }

        [HttpPost]
        public ActionResult Add(CommunitityAddNewModel communitityAddNew)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = Web.Common.WebCommonHelper.GetValidMsg(ModelState)
                });
            }
            CommunitityService.AddNewCommunitity(communitityAddNew.Name, communitityAddNew.RegionId,
                communitityAddNew.Location, communitityAddNew.Traffic, communitityAddNew.BuiltYear);
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            long? userId = AdminHelper.GetUserId(this.HttpContext);
            if (userId == null)
            {
                return Redirect("~/Main/Login");
            }
            long? cityId = AdminHelper.GetCityId(this.HttpContext);
            if (cityId == null)
            {
                return View("~/Views/Shared/Error.cshtml", (object)"总部人员无法管理小区！");
            }
            var communitity = CommunitityService.GetById(id);
            var regions = RegionService.GetAll(cityId.Value);
            CommunitityGetViewModel communitityGetView = new CommunitityGetViewModel();
            communitityGetView.Communitity = communitity;
            communitityGetView.Regions = regions;
            return View(communitityGetView);
        }

        [HttpPost]
        public ActionResult Edit(CommunitityEditModel communitityEdit)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "ok",
                    Msg = Web.Common.WebCommonHelper.GetValidMsg(ModelState)
                });
            }
            CommunitityService.UpdateCommunitity(communitityEdit.Id, communitityEdit.Name, communitityEdit.RegionId, communitityEdit.Location, communitityEdit.Traffic, communitityEdit.BuiltYear);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            CommunitityService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult BatchDelete(long[] ids)
        {
            foreach (long id in ids)
            {
                CommunitityService.MarkDeleted(id);
            }

            return Json(new AjaxResult { Status = "ok" });
        }
    }
}