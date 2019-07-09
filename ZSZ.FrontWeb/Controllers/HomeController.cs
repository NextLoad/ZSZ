using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using ZSZ.FrontWeb.App_Start;
using ZSZ.FrontWeb.Models;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.FrontWeb.Controllers
{
    public class HomeController : Controller
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        public IUser UserService { get; set; }

        public IdName IdNameService { get; set; }

        public ICity CityService { get; set; }
        public ActionResult Index()
        {
            long cityId = FrontHelper.GetCityId(this.HttpContext);
            var cities = CityService.GetAll();
            var types = IdNameService.GetByTypeName("房屋类别");
            HomeIndexViewModel homeIndexView = new HomeIndexViewModel();
            homeIndexView.Cities = cities;
            homeIndexView.Types = types;
            return View(homeIndexView);
        }

        public ActionResult SwitchCityId(long cityId)
        {
            long? userId = FrontHelper.GetUserId(this.HttpContext);
            if (userId == null)
            {
                FrontHelper.SetCityId(this.HttpContext, cityId);
            }
            else
            {
                UserService.SetUserCityId(userId.Value, cityId);
            }

            return Json(new AjaxResult { Status = "ok" });
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            //判断用户是否成功登陆
            if (UserService.CheckLogin(login.PhoneNum, login.Password))
            {
                //登陆成功后，将用户信息存入session中
                var user = UserService.GetByPhoneNum(login.PhoneNum);
                FrontHelper.SetUserId(this.HttpContext, user.Id);
                FrontHelper.SetCityId(this.HttpContext, user.CityId);
                return Json(new AjaxResult { Status = "ok" });
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

            //防止在发送验证码之后 用户修改了手机号码
            if (register.PhoneNum != (string)TempData["phoneNum"])
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = "手机号与注册时的手机号码不一致！"
                });
            }

            if (register.SmsCode != (int)TempData["smsCode"])
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