using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Web.Common
{
    public class CommonPageBar
    {
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页页码
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 显示的最大页码数量
        /// </summary>
        public int MaxPageCount { get; set; }

        /// <summary>
        /// 当前页的页码样式
        /// </summary>
        public string CurrentPageClassName { get; set; }

        /// <summary>
        /// 链接的格式
        /// </summary>
        public string UrlPattern { get; set; }

        /// <summary>
        /// 生成分页的html字符串
        /// </summary>
        /// <returns></returns>
        public string GetPagerHtml()
        {
            StringBuilder pageHtml = new StringBuilder();

            //总页数
            int pageCount = (int)Math.Ceiling(TotalCount * 1.0 / PageSize);

            //起始页
            int startPage = Math.Max(1, CurrentPage - MaxPageCount / 2);

            //结束页
            int endPage = Math.Min(pageCount, startPage + MaxPageCount);

            pageHtml.Append("<ul>");

            string href = string.Empty;
            //如果不是是第一页
            if (CurrentPage != 1)
            {
                href = UrlPattern.Replace("{pn}", "1");
                pageHtml.Append("<li><a href='").Append(href).Append("'>首页</a></li>");
                href = UrlPattern.Replace("{pn}", (CurrentPage - 1).ToString());
                pageHtml.Append("<li><a href='").Append(href).Append("'>上一页</a></li>");
            }


            for (int i = startPage; i <= endPage; i++)
            {


                if (i == CurrentPage)
                {
                    pageHtml.Append("<li class = '").Append(CurrentPageClassName).Append("'>").Append(i)
                        .Append("</li>");
                }
                else
                {
                    href = UrlPattern.Replace("{pn}", i.ToString());
                    pageHtml.Append("<li><a href='").Append(href)
                        .Append("'>").Append(i).Append("</a></li>");
                }
            }


            //如果不是最后一页
            if (CurrentPage != pageCount)
            {
                href = UrlPattern.Replace("{pn}", (CurrentPage + 1).ToString());
                pageHtml.Append("<li><a href='").Append(href).Append("'>下一页</a></li>");
                href = UrlPattern.Replace("{pn}", pageCount.ToString());
                pageHtml.Append("<li><a href='").Append(href).Append("'>末页</a></li>");
            }

            pageHtml.Append("</ul>");
            return pageHtml.ToString();
        }
    }
}
