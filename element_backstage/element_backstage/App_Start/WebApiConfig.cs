using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft;
using System.Windows;

namespace element_backstage
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            /*********** 启用cors ************/
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            /********************************/

            //清除所有的格式
            //config.Formatters.Clear();
            //所有的格式变成json
            //config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            //WebApi 返回小驼峰式 json 格式，并格式化日期
            ConfigureApi(config);
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void ConfigureApi(HttpConfiguration config)
        {
            var jsonFormatter = new JsonMediaTypeFormatter();
            var setting = jsonFormatter.SerializerSettings;

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式
            timeConverter.DateTimeFormat = "yyy'-'MM'-'dd' 'HH':'mm':'ss";
            setting.Converters.Add(timeConverter);

            setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Services.Replace(typeof(IContentNegotiator), new JsonCotentNegotiator(jsonFormatter));
            
        }
    }
}

/// <summary>
/// 
/// </summary>
public class JsonCotentNegotiator: IContentNegotiator
{
    private readonly JsonMediaTypeFormatter _jsonFormatter;
    public JsonCotentNegotiator(JsonMediaTypeFormatter formatter)
    {
        _jsonFormatter = formatter;
    }

    public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage requset, IEnumerable<MediaTypeFormatter> formatters)
    {
        var result = new ContentNegotiationResult(_jsonFormatter, new System.Net.Http.Headers.MediaTypeHeaderValue("applications/json"));
        return result;
    }
}
