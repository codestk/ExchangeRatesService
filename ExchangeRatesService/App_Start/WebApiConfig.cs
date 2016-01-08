using ExchangeService.Code;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ExchangeRatesService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            string SiteList = Config.EnableCorsAttribute();
            var corsAttr = new EnableCorsAttribute(SiteList, "*", "*");
            config.EnableCors(corsAttr);
            //config.EnableCors();

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "ExchangeService/api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // Web API configuration and services

            //Web API routes==============================================================================================
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "ExchangeService/api/{controller}"

            //);

            //config.Routes.MapHttpRoute("DefaultApi", "ExchangeService/api/{controller}/{action}", new { action = "Get" }
            //    );

            //Compress All Data to response===============================================================================
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));

            // Remove the JSON formatter ==================================================================================
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            // Remove the XML formatter
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            //================================================================================Compress All Data to response

            //Cache=======================================================================================================
            // [CacheOutput(ClientTimeSpan = 180, ServerTimeSpan = 180, AnonymousOnly = true)]
            //            config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { action = "get", id = RouteParameter.Optional }
            //);
            //=========================================================================================================Cache
        }
    }
}