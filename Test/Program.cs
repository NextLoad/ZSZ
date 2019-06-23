using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using ZSZ.Services;
using ZSZ.Web.Common;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ZSZDbContext ctx = new ZSZDbContext())
            //{
            //   ctx.Database.Create();
            //}
            Console.WriteLine("ok");
            Console.ReadKey();
        }


        static void Main2(string[] args)
        {
            CommonPageBar commonPage = new CommonPageBar();
            commonPage.CurrentPage = 2;
            commonPage.CurrentPageClassName = "sa";
            commonPage.MaxPageCount = 10;
            commonPage.PageSize = 10;
            commonPage.TotalCount = 120;
            commonPage.UrlPattern = "{pn}";
            string html = commonPage.GetPagerHtml();
            Console.WriteLine(html);
            Console.ReadKey();
        }
        static void Main1(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();


            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
