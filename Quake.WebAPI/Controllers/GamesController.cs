using Quake.Applications.Services;
using Quake.CQS.Contracts;
using Quake.Persistence.Database;
using Quake.Persistence.Repository;
using Quake.Persistence.Transactions;
using System;
using System.Net;
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
            rankingOfGames = new RankingOfGames(new UnitOfWork(new QuakeContext()));
            gameQuake = new GameQuake(rankingOfGames);
        }

        [HttpPost]
        [Route("api/FindRankingOfGamesOfPlayersBy")]
        public Task<HttpResponseMessage> Post([FromBody]dynamic body)
        {
            try
            {
                var name = (string)body.name;
                var retorno = gameQuake.FindRankingOfGamesOfPlayersBy(name);

                return CreateResponse(HttpStatusCode.OK, retorno);
            }
            catch (Exception error)
            {
                return CreateResponse(HttpStatusCode.BadRequest, error.InnerException);
            }
        }
    }
}
