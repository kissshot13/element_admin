using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Linq;
using System.Web.SessionState;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web;

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
            //config.Formatters.Remove(config.Formatters.FirstOrDefault(p => p.GetType() == typeof(System.Web.Http.ModelBinding.JQueryMvcFormUrlEncodedFormatter)));  

            //WebApi 返回小驼峰式 json 格式，并格式化日期
            ConfigureApi(config);
            // Web API 路由
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
             ).RouteHandler = new SessionControllerRouterHandler();
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
            //替换原生的contentNegotiator
            config.Services.Replace(typeof(IContentNegotiator), new JsonCotentNegotiator(jsonFormatter));
            
        }
    }
}

/// <summary>
/// 创建一个替代原生contetnNegotiator的类，返回格式为json
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


public class SessionRouterHandler:HttpControllerHandler, IRequiresSessionState
{
    public SessionRouterHandler(RouteData routeData) : base(routeData)
    {

    }
}

public class SessionControllerRouterHandler : HttpControllerRouteHandler
{
    protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
        return new SessionRouterHandler(requestContext.RouteData);
    }
}