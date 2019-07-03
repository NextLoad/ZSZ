using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZSZ.FrontWeb.Filters;
using ZSZ.Web.Common;

namespace ZSZ.FrontWeb
{
    public class FilterConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilter());
            filters.Add(new JsonNetActionFilter());
        }
    }
}
