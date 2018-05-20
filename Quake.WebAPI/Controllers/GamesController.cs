using Quake.Applications.Services;
using Quake.CQRS.Contracts;
using Quake.Persistence.Repository;
using Quake.Persistence.Transactions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Quake.WebAPI.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IRankingOfGames rankingOfGames;
        private readonly GameQuake gameQuake;

        public GamesController()
        {
            rankingOfGames = new RankingOfGames(new UnitOfWork());
            gameQuake = new GameQuake(rankingOfGames);
        }

        [HttpPost]
        [Route("api/FindRankingOfGamesOfPlayersBy")]
        public Task<HttpResponseMessage> Post([FromBody]dynamic body)
        {
            var name = (string)body.name;

            var retorno = gameQuake.FindRankingOfGamesOfPlayersBy(name);

            return CreateResponse(System.Net.HttpStatusCode.Created, retorno);
        }
    }
}
