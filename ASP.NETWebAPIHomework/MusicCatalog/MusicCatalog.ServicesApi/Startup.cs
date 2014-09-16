using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MusicCatalog.ServicesApi.Startup))]

namespace MusicCatalog.ServicesApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //http://social.msdn.microsoft.com/Forums/vstudio/en-US/a5adf07b-e622-4a12-872d-40c753417645/web-api-error-the-objectcontent1-type-failed-to-serialize-the-response-body-for-content?forum=wcf
        }
    }
}
