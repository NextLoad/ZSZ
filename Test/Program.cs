using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using log4net.Config;
using ZSZ.Services;
using ZSZ.Web.Common;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(IPAddress.Loopback, 11211));
            config.Protocol = MemcachedProtocol.Binary;
            MemcachedClient client = new MemcachedClient(config);
            
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, "p", "123");//还可以指定第四个
            string p1 = client.Get<string>("p");
        }
        static void Main3(string[] args)
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
