using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IServices;

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

        public static long GetCityId(HttpContextBase httpContextBase)
        {
            long? userId = GetUserId(httpContextBase);
            //用户没有登录
            if (userId == null)
            {
                long? cityId = (long?)httpContextBase.Session["cityId"];
                if (cityId != null)
                {
                    return cityId.Value;
                }
                //返回城市表中的第一个城市ID
                var citySvc = DependencyResolver.Current.GetService<ICity>();
                return citySvc.GetAll()[0].Id;
            }
            //用户已经登录
            else
            {
                var userSvc = DependencyResolver.Current.GetService<IUser>();
                var user = userSvc.GetById(userId.Value);
                if (user.CityId != null)
                {
                    return user.CityId.Value;
                }
                
                //返回城市表中的第一个城市ID
                var citySvc = DependencyResolver.Current.GetService<ICity>();
                return citySvc.GetAll()[0].Id;
            }


        }

        public static void SetCityId(HttpContextBase httpContextBase, long? cityId)
        {
            httpContextBase.Session["cityId"] = cityId;
        }

    }
}