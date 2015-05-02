
using System.Data.Entity;
using System.Globalization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineHotelReservationSystem
{
    

   public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Database.SetInitializer<Areas.Customer.Models.ReservationDbContext>(null);
            Database.SetInitializer<Areas.Customer.Models.UserLoginDbContext>(null);
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_BeginRequest()
        {

            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString()); info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"; System.Threading.Thread.CurrentThread.CurrentCulture = info;

        }
    }
}