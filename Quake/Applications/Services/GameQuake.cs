using Quake.CQS;
using Quake.CQS.Contracts;
using System.Collections.Generic;

namespace Quake.Applications.Services
{
    public class GameQuake
    {
        private IRankingOfGames rankingOfGames;

        public GameQuake(IRankingOfGames rankingOfGames)
        {
            this.rankingOfGames = rankingOfGames;
        }

        public List<KillsByPlayers> FindRankingOfGamesOfPlayersBy(string name)
        {
            return rankingOfGames.FindPlayerBy(name);
        }
    }
}
