using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApiExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //regresa todo en json serializado
            var formmater = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formmater.SerializerSettings.ContractResolver 
                = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}
