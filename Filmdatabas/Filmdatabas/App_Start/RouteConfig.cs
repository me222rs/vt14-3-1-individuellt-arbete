using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Filmdatabas.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Home2",
                "",
                "~/Pages/Home2.aspx");

            //routes.MapPageRoute("Add2",
            //    "Movie/Add",
            //    "~/Pages/Add2.aspx");
        }
    }
}