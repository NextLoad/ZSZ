using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace ZSZ.FrontWeb.Filters
{
    public class ExceptionFilter
    {
        private ILog log = LogManager.GetLogger(typeof(ExceptionFilter));
        public void OnException(ExceptionContext filterContext)
        {
            log.Error("前台未经处理的异常", filterContext.Exception);
        }
    }
}