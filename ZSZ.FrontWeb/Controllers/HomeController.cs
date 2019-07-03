using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using ZSZ.FrontWeb.Models;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.FrontWeb.Controllers
{
    public class HomeController : Controller
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        public IUser UserService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (UserService.CheckLogin(login.PhoneNum, login.Password))
            {
                return RedirectToAction(nameof(Index));
            }

            return Json(new AjaxResult { Status = "fail", Msg = "用户名或密码错误！" });
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel register)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = WebCommonHelper.GetValidMsg(ModelState)
                });
            }

            if (register.PhoneNum != (string)TempData["phoneNum"])
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = "手机号与注册时的手机号码不一致！"
                });
            }

            if (register.SmsCode != (int) TempData["smsCode"])
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = "短信验证码错误！"
                });
            }

            var user = UserService.GetByPhoneNum(register.PhoneNum);
            if (user != null)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = "该手机号已经注册！"
                });
            }

            UserService.AddNewUser(register.PhoneNum, register.Password, register.CityId);
            return Json(new AjaxResult { Status = "ok" });
        }

        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVerifyCode()
        {
            string verifyCode = WebCommonHelper.CreateVerifyCode(5);
            TempData["verifyCodeFront"] = verifyCode;
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 51, 100, 20, 6);
            return File(ms, "image/jpeg");
        }


        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult SendSmsCode(string phoneNum, string verifyCode)
        {
            if (TempData["verifyCodeFront"] == null || verifyCode != (string)TempData["verifyCodeFront"])
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = "验证码错误"
                });
            }

            TempData["phoneNum"] = phoneNum;
            int smsCode = new Random().Next(1000, 9999);
            TempData["smsCode"] = smsCode;
            log.InfoFormat("短信验证码是{0}", smsCode);
            return Json(new AjaxResult
            {
                Status = "ok"
            });
        }
    }
}