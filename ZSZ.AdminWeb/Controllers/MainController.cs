using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using ZSZ.AdminWeb.App_Start;
using ZSZ.AdminWeb.Models;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.AdminWeb.Controllers
{
    public class MainController : Controller
    {
        public IAdminUser AdminUserService { get; set; }

        public ActionResult Index()
        {
            long? userId = AdminHelper.GetUserId(this.HttpContext);
            if (userId == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var adminUser = AdminUserService.GetById(userId.Value);
            return View(adminUser);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = Web.Common.WebCommonHelper.GetValidMsg(ModelState)
                });
            }
            if (TempData["verifyCode"] == null || loginModel.VerifyCode != (string)TempData["verifyCode"])
            {
                return Json(new AjaxResult { Status = "error", Msg = "验证码错误" });
            }

            var isLogin = AdminUserService.CheckLogin(loginModel.PhoneNum, loginModel.Password);
            if (isLogin)
            {
                var adminUser = AdminUserService.GetByPhoneNum(loginModel.PhoneNum);
                AdminHelper.SetUserId(this.HttpContext, adminUser.Id);
                AdminHelper.SetCityId(this.HttpContext,adminUser.CityId);
                return Json(new AjaxResult { Status = "ok" });
            }

            return Json(new AjaxResult { Status = "error", Msg = "用户名或密码错误" });
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction(nameof(Login));
        }

        public ActionResult CreateVerifyCode()
        {
            string verifyCode = Web.Common.WebCommonHelper.CreateVerifyCode(4);
            TempData["verifyCode"] = verifyCode;
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 51, 100, 20, 6);
            return File(ms, "image/jpeg");

        }
    }
}