using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using ZSZ.AdminWeb.Models;
using ZSZ.IServices;

namespace ZSZ.AdminWeb.Controllers
{
    public class MainController : Controller
    {
        public IAdminUser AdminUserService { get; set; }
        // GET: Main

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (TempData["verifyCode"] == null)
            {
                return Content("验证码错误！");
            }

            if (loginModel.VerifyCode != (string)TempData["verifyCode"])
            {
                return Content("验证码错误！");
            }

            var isLogin = AdminUserService.CheckLogin(loginModel.PhoneNum, loginModel.Password);
            if (isLogin)
            {
                Session["userInfo"] = AdminUserService.GetByPhoneNum(loginModel.PhoneNum);
                return Content("登录成功！");
            }
            else
            {
                return Content("用户名或密码错误");
            }
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