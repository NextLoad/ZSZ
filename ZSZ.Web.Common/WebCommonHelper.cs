using System;
using System.Collections.Generic;
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
            char[] data = { 'a','c','d','e','f','h','k','m',
                'n','r','s','t','w','x','y'};
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                int index = rand.Next(data.Length);//[0,data.length)
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
    }
}
