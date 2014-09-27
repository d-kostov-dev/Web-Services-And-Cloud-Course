using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Ninject;
using System.Reflection;
using Application.Data.Interfaces;
using Application.Data;
using Application.WebApi.Providers;

using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

[assembly: OwinStartup(typeof(Application.WebApi.Startup))]

namespace Application.WebApi
{
    public partial class Startup
    {
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
            kernel.Bind<IDataProvider>().To<DataProvider>()
                .WithConstructorArgument("context",
                    c => new ApplicationDbContext());

            kernel.Bind<IUserIdProvider>().To<UserIdProvider>();
        }
    }
}
