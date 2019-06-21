using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.AdminWeb.App_Start
{
    public class AdminHelper
    {
        public static long? GetUserId(HttpContextBase httpContextBase)
        {
            long? userId = (long?) httpContextBase.Session["userId"];
            return userId;
        }

        public static void SetUserId(HttpContextBase httpContextBase,long userId)
        {
            httpContextBase.Session["userId"] = userId;
        }
    }
}