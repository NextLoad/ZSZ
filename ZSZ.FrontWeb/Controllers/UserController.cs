using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.FrontWeb.Controllers
{
    public class UserController : Controller
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserController));
        public IUser UserService { get; set; }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string phoneNum, string verifyCode)
        {
            if (verifyCode != (string)TempData["verifyCodeFront"])
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = "验证码错误！"
                });
            }

            var user = UserService.GetByPhoneNum(phoneNum);
            if (user == null)
            {
                return Json(new AjaxResult
                {
                    Status = "error",
                    Msg = "没有该手机号的用户！"
                });
            }

            Session["forgetPhoneNum"] = phoneNum;
            return Json(new AjaxResult { Status = "ok" });
        }

        public ActionResult SendSmsCode(string forgetPhoneNum)
        {
            if (forgetPhoneNum != (string)Session["forgetPhoneNum"])
            {
                return Json(new AjaxResult { Status = "error", Msg = "手机号码与输入的不一致！" });
            }
            int smsCode = new Random().Next(1000, 9999);
            TempData["smsCode"] = smsCode;
            log.InfoFormat("短信验证码是{0}", smsCode);
            return Json(new AjaxResult
            {
                Status = "ok"
            });
        }

        [HttpGet]
        public ActionResult ForgetPassword2()
        {
            return View(Session["forgetPhoneNum"]);
        }

        [HttpPost]
        public ActionResult ForgetPassword2(string forgetPhoneNum, int smsCode)
        {
            if (forgetPhoneNum != (string)Session["forgetPhoneNum"])
            {
                return Json(new AjaxResult { Status = "error", Msg = "手机号码与输入的不一致！" });
            }

            if (smsCode != (int)TempData["smsCode"])
            {
                return Json(new AjaxResult { Status = "error", Msg = "手机验证码错误" });
            }

            TempData["IsSmsCode_OK"] = true;
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult ForgetPassword3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword3(string password, string password2)
        {
            //漏洞，一定要通过验证的才能修改密码，否则只要知道账户就能随便修改密码
            if (true != (bool?)TempData["IsSmsCode_OK"])
            {
                return Json(new AjaxResult { Status = "error", Msg = "未通过短信验证码验证，无法修改" });
            }
            if (password != password2)
            {
                return Json(new AjaxResult { Status = "error", Msg = "两次输入的密码不一致" });
            }

            string phoneNum = (string)Session["forgetPhoneNum"];
            var user = UserService.GetByPhoneNum(phoneNum);
            UserService.UpdateUser(user.PhoneNum, password, user.CityId);
            Session["forgetPhoneNum"] = null;
            return Json(new AjaxResult { Status = "ok" });
        }

        [HttpGet]
        public ActionResult ForgetPassword4()
        {
            return View();
        }
    }
}