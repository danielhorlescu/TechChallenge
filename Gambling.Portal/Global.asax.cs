using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

namespace Gambling.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureAutoMapper();
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(c => { c.AddProfile(new PresentationMappingProfile()); });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
