using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UploadFilesApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
        
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{v}",
                defaults: new { id = RouteParameter.Optional, v = RouteParameter.Optional }
            );
        }
    }
}
