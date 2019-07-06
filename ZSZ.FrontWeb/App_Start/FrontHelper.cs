using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.FrontWeb.App_Start
{
    public class FrontHelper
    {

        public static long? GetUserId(HttpContextBase httpContextBase)
        {
            long? userId = (long?)httpContextBase.Session["userId"];
            return userId;
        }

        public static void SetUserId(HttpContextBase httpContextBase, long userId)
        {
            httpContextBase.Session["userId"] = userId;
        }

        public static long? GetCityId(HttpContextBase httpContextBase)
        {
            long? cityId = (long?)httpContextBase.Session["cityId"];
            return cityId;
        }

        public static void SetCityId(HttpContextBase httpContextBase, long? cityId)
        {
            httpContextBase.Session["cityId"] = cityId;
        }

    }
}