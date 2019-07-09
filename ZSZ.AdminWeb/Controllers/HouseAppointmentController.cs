using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.AdminWeb.Controllers
{
    public class HouseAppointmentController : Controller
    {
        public IHouseAppointment HouseAppointmentService { get; set; }
        public ActionResult List()
        {
            var appointments = HouseAppointmentService.GetAll();
            return View(appointments);
        }

        public ActionResult Follow(long appId)
        {
            bool isSuccess = HouseAppointmentService.Follow(AdminHelper.GetUserId(this.HttpContext).Value,
                appId);
            if (isSuccess)
            {
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "fail" });
            }
        }

    }
}