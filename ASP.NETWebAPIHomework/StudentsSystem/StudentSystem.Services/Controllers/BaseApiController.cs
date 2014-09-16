namespace StudentSystem.Services.Controllers
{
    using System.Web.Http;

    using StudentsSystem.Data.Interfaces;

    public abstract class BaseApiController : ApiController
    {
        protected IDbData data;

        public BaseApiController(IDbData data)
        {
            this.data = data;
        }
    }
}