namespace Application.Services.Tests
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;

    public static class ControllerSetup
    {
        public static void SetupController(ApiController controller, string controllerType, string routeParameter = "api/{controller}/{id}")
        {
            string serverUrl = "http://test-url.com";

            // Setup the Request object of the controller
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(serverUrl)
            };

            controller.Request = request;

            // Setup the configuration of the controller
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: routeParameter,
                defaults: new { id = RouteParameter.Optional });

            controller.Configuration = config;

            // Apply the routes of the controller
            controller.RequestContext.RouteData =
                new HttpRouteData(
                    route: new HttpRoute(),
                    values: new HttpRouteValueDictionary
                    {
                        { "controller", controllerType }
                    });
        }
    }
}
