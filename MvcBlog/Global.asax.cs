using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);         
            RouteTable.Routes.MapRoute("HavaDurumu", "havadurumu/{il}",new { controller = "Web", action = "HavaDurumu", il = (string)null }
);
        }
    }
}
