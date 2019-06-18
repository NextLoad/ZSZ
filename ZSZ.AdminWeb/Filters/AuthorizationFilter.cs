using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
using ZSZ.DTO;
using ZSZ.IServices;
using ZSZ.Web.Common;

namespace ZSZ.AdminWeb.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //获取在方法上标记了CheckPermissionAttribute的所有特性标签
            CheckPermissionAttribute[] checks =
                filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckPermissionAttribute), false) as
                    CheckPermissionAttribute[];
            if (checks == null || checks.Length <= 0) return;
            AdminUserDTO adminUser = filterContext.HttpContext.Session["userInfo"] as AdminUserDTO;
            long? userid = 0;
            //没有登录
            if (adminUser == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AjaxResult ajaxResult = new AjaxResult
                    {
                        Status = "error",
                        Msg = "您没有登录",
                        Data = "/Main/Login"
                    };
                    var result = new JsonNetResult();
                    result.Data = ajaxResult;
                    filterContext.Result = result;
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Main/Login");
                }

                return;
            }
            userid = (long?)(adminUser).Id;


            IAdminUser adminUserService = DependencyResolver.Current.GetService<IAdminUser>();

            foreach (var check in checks)
            {
                //检查用户是否具有相应的权限
                bool hasPerm = adminUserService.HasPermission(userid.Value, check.Permission);
                if (!hasPerm)
                {
                    //如果是ajax请求
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        AjaxResult ajaxResult = new AjaxResult
                        {
                            Status = "error",
                            Msg = "您没有权限" + check.Permission + "访问"
                        };
                        var result = new JsonNetResult();
                        result.Data = ajaxResult;
                        filterContext.Result = result;
                    }
                    else
                    {
                        ContentResult result = new ContentResult();
                        result.Content = "您没有权限" + check.Permission + "访问";
                        filterContext.Result = result;
                    }
                    return;
                }
            }
        }

    }
}