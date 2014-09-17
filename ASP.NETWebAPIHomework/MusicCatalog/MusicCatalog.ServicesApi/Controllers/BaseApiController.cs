namespace MusicCatalog.ServicesApi.Controllers
{
    using System.Web.Http;

    using MusicCatalog.Data.Interfaces;

    public abstract class BaseApiController : ApiController
    {
        protected IAppData data;

        public BaseApiController(IAppData data)
        {
            this.data = data;
        }
    }
}