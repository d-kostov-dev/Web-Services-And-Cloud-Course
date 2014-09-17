using System.Reflection;
using System.Web.Http;

using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

using MusicCatalog.Data;
using MusicCatalog.Data.Interfaces;
using MusicCatalog.Data.Repositories;

[assembly: OwinStartup(typeof(MusicCatalog.ServicesApi.Startup))]

namespace MusicCatalog.ServicesApi
{
    public partial class Startup
    {
        //http://social.msdn.microsoft.com/Forums/vstudio/en-US/a5adf07b-e622-4a12-872d-40c753417645/web-api-error-the-objectcontent1-type-failed-to-serialize-the-response-body-for-content?forum=wcf

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterMappings(kernel);
            return kernel;
        }

        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IAppData>().To<AppData>()
                .WithConstructorArgument("context",
                    c => new MusicCatalogDbContext());
        }
    }
}
