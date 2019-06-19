using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZSZ.AdminWeb.App_Start
{
    public class FilterConfig
    {
        public static void RegistFilter(GlobalFilterCollection filters)
        {
            filters.Add(new Filters.ExceptionFilter());
            filters.Add(new Web.Common.JsonNetActionFilter());
            filters.Add(new Filters.AuthorizationFilter());
        }
    }
}