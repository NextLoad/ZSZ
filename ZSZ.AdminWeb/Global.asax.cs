using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using ZSZ.AdminWeb.ModelBinders;
using ZSZ.IServices;

namespace ZSZ.AdminWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());

            GlobalFilters.Filters.Add(new Filters.ExceptionFilter());
            GlobalFilters.Filters.Add(new Web.Common.JsonNetActionFilter());

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            //把当前程序集中的 Controller 都注册
            //不要忘了.PropertiesAutowired()
            // 获取所有相关类库的程序集
            Assembly[] assemblies = new Assembly[] { Assembly.Load("ZSZ.Services") };
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => !type.IsAbstract && typeof(IServiceAutofac).IsAssignableFrom(type))
                .AsImplementedInterfaces().PropertiesAutowired();
            var container = builder.Build();
            //注册系统级别的 DependencyResolver，这样当 MVC 框架创建 Controller 等对象的时候都是管 Autofac 要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
