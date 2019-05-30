using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Common
{
    public static class CommonHelper
    {
        /**/
        ///
        /// 转半角的函数(DBC case)
        ///
        ///任意字符串
        ///半角字符串
        ///
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///
        public static String ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }

        /// <summary>
        /// 计算MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CalcMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                sb.Append(md5Bytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
