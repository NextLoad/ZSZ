using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Common;

namespace ZSZ.AdminWeb.ModelBinders
{
    public class TrimToDBCModelBinder :DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object value = base.BindModel(controllerContext, bindingContext);
            if (value is string)
            {
                string item = (string) value;
                item = item.ToDBC().Trim();
                return item;
            }
            else
            {
                return value;
            }
        }
    }
}