namespace Application.WebApi
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Microsoft.Owin.Security.OAuth;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GuessRoute",
                routeTemplate: "api/games/{id}/guess",
                defaults: new { id = RouteParameter.Optional, controller = "Guesses", });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var corsOptions = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsOptions);

            // Uncomment if needed
            ////config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            ////config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
