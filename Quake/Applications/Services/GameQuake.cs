﻿using Quake.CQRS;
using Quake.CQRS.Contracts;
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
            try
            {
                return rankingOfGames.FindPlayerBy(name);
            }
            catch
            {
                throw;
            }
        }
    }
}
