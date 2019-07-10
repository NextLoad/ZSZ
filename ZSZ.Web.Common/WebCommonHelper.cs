using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZSZ.Web.Common
{
    public class WebCommonHelper
    {
        //Chapcha
        public static string CreateVerifyCode(int len)
        {
            char[] data =
            {
                'a', 'c', 'd', 'e', 'f', 'h', 'k', 'm',
                'n', 'r', 's', 't', 'w', 'x', 'y'
            };
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = rand.Next(data.Length); //[0,data.length)
                char ch = data[index];
                sb.Append(ch);
            }

            //勤测！
            return sb.ToString();
        }

        public static string GetValidMsg(ModelStateDictionary modelState)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count <= 0)
                {
                    continue;
                }

                sb.Append("属性【").Append(key).Append("】错误：");
                foreach (var modelError in modelState[key].Errors)
                {
                    sb.AppendLine(modelError.ErrorMessage);
                }
            }

            return sb.ToString();
        }

        public static string ToQueryString(NameValueCollection nvc)
        {
            if (nvc == null)
            {
                throw new ArgumentNullException("nvc");
            }
            StringBuilder sb = new StringBuilder();
            foreach (string key in nvc.AllKeys)
            {
                sb.Append(key).Append("=").Append(Uri.EscapeDataString(nvc[key])).Append("&");
            }

            return sb.ToString().TrimEnd('&');
        }

        public static string UpdateQueryString(NameValueCollection nvc, string key, string value)
        {
            if (nvc == null)
            {
                throw new ArgumentNullException("nvc");
            }
            NameValueCollection newNvc = new NameValueCollection(nvc);
            //如果原先的nvc包含了该key，则更新
            if (newNvc.AllKeys.Contains(key))
            {
                newNvc[key] = value;
            }
            else
            {
                newNvc.Add(key, value);
            }

            return ToQueryString(newNvc);
        }

        public static string RemoveQueryString(NameValueCollection nvc, string key)
        {
            if (nvc == null)
            {
                throw new ArgumentNullException("nvc");
            }
            NameValueCollection newNvc = new NameValueCollection(nvc);
            //如果原先的nvc包含了该key，则更新
            if (newNvc.AllKeys.Contains(key))
            {
                newNvc.Remove(key);
            }

            return ToQueryString(newNvc);
        }

        public static string RendViewToString(ControllerContext context, string viewName, string masterName)
        {
            //找到对应控制器下的视图
            ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(context, viewName, masterName);
            if (viewEngineResult == null)
            {
                throw new Exception("找不到视图" + viewName);
            }
            IView view = viewEngineResult.View;
            using (StringWriter sw = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                //会将layout页面的其他信息也写入。
                view.Render(viewContext, sw);
                return sw.ToString();
            }


        }
    }
}
