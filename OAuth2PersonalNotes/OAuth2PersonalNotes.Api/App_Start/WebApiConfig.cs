﻿using System.Web.Http;

namespace OAuth2PersonalNotes.Api
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});

            return config;
        }
    }
}