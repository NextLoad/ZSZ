using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
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
            ViewBag.HouseId = houseId;
            return View(housePics);
        }

        public ActionResult HousePicAdd(long houseId)
        {
            return View(houseId);
        }

        public ActionResult FileUpload(long houseId, HttpPostedFileBase file)
        {
            //获取文件的后缀名
            string md5Name = Common.CommonHelper.CalcMD5(file.InputStream);
            string extension = Path.GetExtension(file.FileName);

            string newFileName = md5Name + extension;
            string thumbFileName = md5Name + "_thumb" + extension;
            string watermarkFileName = md5Name + "_watermark" + extension;
            //原图文件路径
            string path = Path.Combine("/UpLoadFile", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(),
                DateTime.Now.Day.ToString(), newFileName);
            //缩略图文件路径
            string thumbPath = Path.Combine("/UpLoadFile", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(),
                DateTime.Now.Day.ToString(), thumbFileName);
            //水印文件路径
            string watermarkPath = Path.Combine("/UpLoadFile", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(),
                DateTime.Now.Day.ToString(), watermarkFileName);
            string fullPath = this.HttpContext.Server.MapPath("~" + path);
            string thumbFullPath = this.HttpContext.Server.MapPath("~" + thumbPath);
            string watermarkFullPath = this.HttpContext.Server.MapPath("~" + watermarkPath);
            //尝试创建文件夹
            new FileInfo(fullPath).Directory.Create();


            //原文件
            file.SaveAs(fullPath);

            //文件指针复位，可能在保存时指针的位置指向了最后
            file.InputStream.Position = 0;
            //缩略图
            ImageProcessingJob jobThumb = new ImageProcessingJob();
            jobThumb.Filters.Add(new FixedResizeConstraint(200, 200));
            jobThumb.SaveProcessedImageToFileSystem(file.InputStream, thumbFullPath);

            //文件指针复位，可能在保存时指针的位置指向了最后
            file.InputStream.Position = 0;
            //水印
            ImageWatermark imgWatermark = new ImageWatermark(HttpContext.Server.MapPath("~/images/watermark.png"));
            imgWatermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;//水印位置
            imgWatermark.Alpha = 50;//透明度，需要水印图片是背景透明的 png 图片
            ImageProcessingJob jobNormal = new ImageProcessingJob();
            jobNormal.Filters.Add(imgWatermark);//添加水印
            jobNormal.Filters.Add(new FixedResizeConstraint(600, 600));//限制图片的大小，避免生成大图。如果想原图大小处理，就不用加这个 Filter
            jobNormal.SaveProcessedImageToFileSystem(file.InputStream, watermarkFullPath);

            HousePicDTO housePic = new HousePicDTO();
            housePic.HouseId = houseId;
            housePic.ThumbUrl = thumbPath;
            housePic.Url = path;
            HousePicService.AddNewHousePic(housePic);


            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult HousePicBatchDelete(long[] seletcedIds)
        {
            foreach (long id in seletcedIds)
            {
                HousePicService.MarkDeleted(id);
            }

            return Json(new AjaxResult { Status = "ok" });
        }

    }
}