using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
using ZSZ.AdminWeb.Models;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.AdminWeb.Controllers
{
    public class HouseController : Controller
    {
        public IHouse HouseService { get; set; }

        public IHousePic HousePicService { get; set; }

        public IdName IdNameService { get; set; }

        public IRegion RegionService { get; set; }

        public ICommunitity CommunitityService { get; set; }

        public ActionResult List(int pageIndex = 1, int pageCount = 10)
        {
            long? userId = AdminHelper.GetUserId(this.HttpContext);
            long? cityId = AdminHelper.GetCityId(this.HttpContext);
            if (cityId == null)
            {
                return View("~/Views/Shared/Error.cshtml", (object)"总部人员无法管理房源！");
            }

            var housees = HouseService.GetAll();
            HouseListViewModel houseListView = new HouseListViewModel();
            houseListView.Housees = housees;
            houseListView.PageCount = pageCount;
            houseListView.PageIndex = pageIndex;
            houseListView.TotalCount = housees.LongLength;
            return View(houseListView);
        }

        [HttpGet]
        public ActionResult Add()
        {
            long? cityId = AdminHelper.GetCityId(this.HttpContext);
            if (cityId == null)
            {
                return View("~/Views/Shared/Error.cshtml", (object)"总部人员无法管理房源！");
            }

            var regions = RegionService.GetAll(cityId.Value);
            //var communitities = CommunitityService.
            var roomTypes = IdNameService.GetByTypeName("户型");
            var status = IdNameService.GetByTypeName("房屋状态");
            var decorateStatus = IdNameService.GetByTypeName("装修状态");
            var types = IdNameService.GetByTypeName("房屋类别");
            HouseAddViewModel houseAddView = new HouseAddViewModel();
            houseAddView.Status = status;
            houseAddView.DecorateStatus = decorateStatus;
            houseAddView.Regions = regions;
            houseAddView.RoomTypes = roomTypes;
            houseAddView.Types = types;
            return View(houseAddView);
        }

        [HttpPost]
        public ActionResult Add(HouseAddNewModel houseAdd)
        {
            HouseDTO house = new HouseDTO();
            house.Address = houseAdd.Address;
            house.Area = houseAdd.Area;
            house.MonthRent = houseAdd.MonthRent;
            house.CheckInDateTime = houseAdd.CheckInDateTime;
            house.CommunityId = houseAdd.CommunityId;
            house.DecorateStatusId = houseAdd.DecorateStatusId;
            house.Description = houseAdd.Description;
            house.Direction = houseAdd.Direction;
            house.FloorIndex = houseAdd.FloorIndex;
            house.LookableDateTime = houseAdd.LookableDateTime;
            house.OwnerName = houseAdd.OwnerName;
            house.OwnerPhoneNum = houseAdd.OwnerPhoneNum;
            house.RegionId = houseAdd.RegionId;
            house.RoomTypeId = houseAdd.RoomTypeId;
            house.StatusId = houseAdd.StatusId;
            house.TotalFloatCount = houseAdd.TotalFloatCount;
            house.TypeId = houseAdd.TypeId;
            HouseService.AddNewHouse(house);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult GetCommunitities(long regionId)
        {
            var communitities = CommunitityService.GetByRegionId(regionId);
            return Json(new AjaxResult() { Status = "ok", Data = communitities });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            long? cityId = AdminHelper.GetCityId(this.HttpContext);
            if (cityId == null)
            {
                return View("~/Views/Shared/Error.cshtml", (object)"总部人员无法管理房源！");
            }
            var house = HouseService.GetById(id);
            var regions = RegionService.GetAll(cityId.Value);
            //var communitities = CommunitityService.
            var roomTypes = IdNameService.GetByTypeName("户型");
            var status = IdNameService.GetByTypeName("房屋状态");
            var decorateStatus = IdNameService.GetByTypeName("装修状态");
            var types = IdNameService.GetByTypeName("房屋类别");
            HouseEditViewModel houseEditView = new HouseEditViewModel();
            houseEditView.House = house;
            houseEditView.Status = status;
            houseEditView.DecorateStatus = decorateStatus;
            houseEditView.Regions = regions;
            houseEditView.RoomTypes = roomTypes;
            houseEditView.Types = types;
            return View(houseEditView);
        }

        [HttpPost]
        public ActionResult Edit(HouseEditModel houseEdit)
        {
            HouseDTO house = new HouseDTO();
            house.Id = houseEdit.Id;
            house.Address = houseEdit.Address;
            house.Area = houseEdit.Area;
            house.MonthRent = houseEdit.MonthRent;
            house.CheckInDateTime = houseEdit.CheckInDateTime;
            house.CommunityId = houseEdit.CommunityId;
            house.DecorateStatusId = houseEdit.DecorateStatusId;
            house.Description = houseEdit.Description;
            house.Direction = houseEdit.Direction;
            house.FloorIndex = houseEdit.FloorIndex;
            house.LookableDateTime = houseEdit.LookableDateTime;
            house.OwnerName = houseEdit.OwnerName;
            house.OwnerPhoneNum = houseEdit.OwnerPhoneNum;
            house.RegionId = houseEdit.RegionId;
            house.RoomTypeId = houseEdit.RoomTypeId;
            house.StatusId = houseEdit.StatusId;
            house.TotalFloatCount = houseEdit.TotalFloatCount;
            house.TypeId = houseEdit.TypeId;
            HouseService.UpdateHouse(house);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            HouseService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult BatchDelete(long[] ids)
        {
            foreach (var id in ids)
            {
                HouseService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult HousePicList(long houseId)
        {
            var housePics = HousePicService.GetPics(houseId);
            return View(housePics);
        }

    }
}