using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace ZSZ.AdminWeb.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private ILog log = LogManager.GetLogger(typeof(ExceptionFilter));
        public void OnException(ExceptionContext filterContext)
        {
            log.Error("后台未经处理的异常", filterContext.Exception);
        }
    }
}