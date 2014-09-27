namespace Application.WebApi.Controllers
{
    using System.Web.Http;

    using Application.Data.Interfaces;
    using Application.WebApi.Providers;

    public class BaseController : ApiController
    {
        protected const string INVALID_ITEM_FORMAT = "Invalid or non-existent {0}!";

        protected IDataProvider data;
        protected IUserIdProvider userIdProvider;

        public BaseController(IDataProvider dataProvider, IUserIdProvider userProvider)
        {
            this.data = dataProvider;
            this.userIdProvider = userProvider;
        }
    }
}
