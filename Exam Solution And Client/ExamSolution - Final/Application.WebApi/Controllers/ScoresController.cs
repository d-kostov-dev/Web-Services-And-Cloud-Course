namespace Application.WebApi.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Application.Data.Interfaces;
    using Application.WebApi.Models;
    using Application.WebApi.Providers;

    public class ScoresController : BaseController
    {
        private const int ItemsToReturn = 10;

        public ScoresController(IDataProvider dataProvider, IUserIdProvider userIdProvider)
            : base(dataProvider, userIdProvider)
        {
        }

        [HttpGet]
        public IHttpActionResult GetTopPlayers()
        {
            var topPlayers = this.data.Users
                .All()
                .OrderByDescending(x => x.Rank)
                .Take(ItemsToReturn)
                .Select(ScoreModel.ViewModel);

            return this.Ok(topPlayers);
        }
    }
}
