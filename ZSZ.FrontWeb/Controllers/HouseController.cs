using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZSZ.Common;
using ZSZ.DTO;
using ZSZ.FrontWeb.App_Start;
using ZSZ.FrontWeb.Models;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.FrontWeb.Controllers
{
    public class HouseController : Controller
    {
        public IHouse HouseService { get; set; }
        public ICity CityService { get; set; }
        public IRegion RegionService { get; set; }

        public IHousePic HousePicService { get; set; }

        public IAttachment AttachmentService { get; set; }

        public IHouseAppointment HouseAppointmentService { get; set; }

        public ActionResult Index(long id = 1)
        {
            #region Cache缓存
            //string cacheKey = "houseIndex_" + id;
            //HouseIndexViewModel houseIndexView = null;
            //if (HttpContext.Cache[cacheKey] == null)
            //{
            //    HouseDTO house = HouseService.GetById(id);
            //    if (house == null)
            //    {
            //        return View("~/Views/Shared/Error.cshtml", (object)"不存在的房源id");
            //    }

            //    var housePics = HousePicService.GetPics(id);
            //    var attachments = AttachmentService.GetAttachment(id);
            //    houseIndexView = new HouseIndexViewModel
            //    {
            //        House = house,
            //        HousePics = housePics,
            //        Attachments = attachments
            //    };


            //    HttpContext.Cache.Insert(cacheKey, houseIndexView, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero);
            //}
            //else
            //{
            //    houseIndexView = (HouseIndexViewModel)HttpContext.Cache[cacheKey];
            //} 
            #endregion

            #region Memcached缓存



            string cacheKey = "houseIndex_" + id;



            HouseIndexViewModel houseIndexView = MemcachedMgr.Instance.GetValue<HouseIndexViewModel>(cacheKey);

            if (houseIndexView == null)
            {
                HouseDTO house = HouseService.GetById(id);
                if (house == null)
                {
                    return View("~/Views/Shared/Error.cshtml", (object)"不存在的房源id");
                }

                var housePics = HousePicService.GetPics(id);
                var attachments = AttachmentService.GetAttachment(id);
                houseIndexView = new HouseIndexViewModel
                {
                    House = house,
                    HousePics = housePics,
                    Attachments = attachments
                };


                MemcachedMgr.Instance.SetValue(cacheKey, houseIndexView, TimeSpan.FromMinutes(1));//还可以指定第四个
            }


            #endregion

            return View(houseIndexView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthRent">*-1000,1001-2000,2000-*</param>
        /// <param name="monthRentStart"></param>
        /// <param name="monthRentEnd"></param>
        private void ParseMonthRent(string monthRent, out int monthRentStart, out int monthRentEnd)
        {
            if (string.IsNullOrEmpty(monthRent))
            {
                monthRentStart = 0;
                monthRentEnd = Int32.MaxValue;
                return;
            }
            string[] monthRents = monthRent.Split('-');
            if (monthRents[0] == "*")
            {
                monthRentStart = 0;
            }
            else
            {
                int.TryParse(monthRents[0], out monthRentStart);
            }

            if (monthRents[1] == "*")
            {
                monthRentEnd = Int32.MaxValue;
            }
            else
            {
                int.TryParse(monthRents[1], out monthRentEnd);
            }
        }

        public ActionResult Search(HouseSearchOptionModel houseSearchOption)
        {
            long cityId = FrontHelper.GetCityId(this.HttpContext);
            CityDTO city = CityService.GetById(cityId);
            RegionDTO[] regions = RegionService.GetAll(cityId);
            int monthRentStart, monthRentEnd;
            ParseMonthRent(houseSearchOption.MonthRent, out monthRentStart, out monthRentEnd);
            HouseSearchOptions houseSearchOptions = new HouseSearchOptions
            {
                CityId = cityId,
                RegionId = houseSearchOption.RegionId,
                KeyWords = houseSearchOption.KeyWords,
                TypeId = houseSearchOption.TypeId,
                MonthRentStart = monthRentStart,
                MonthRentEnd = monthRentEnd
            };
            var houses = HouseService.Search(houseSearchOptions);

            HouseSearchViewModel houseSearchView = new HouseSearchViewModel
            {
                City = city,
                Regions = regions,
                Houses = houses
            };
            return View(houseSearchView);
        }

        [HttpPost]
        public ActionResult MakeAppointment(string name, string phoneNum, DateTime visitDate, long houseId)
        {
            HouseAppointmentService.AddNewHouseAppointment(FrontHelper.GetUserId(this.HttpContext),
                name, phoneNum, visitDate, houseId);
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}