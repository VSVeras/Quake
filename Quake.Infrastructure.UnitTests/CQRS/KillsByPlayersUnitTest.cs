using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.CQRS;
using Quake.Infrastructure.Infrastructure.Readers;
using Quake.Persistence.Database;
using Quake.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quake.Infrastructure.UnitTests.CQRS
{
    [TestClass]
    public class KillsByPlayersUnitTest
    {
        private static readonly string logFilePath = Environment.CurrentDirectory + @"\Container\games.log";

        static KillsByPlayersUnitTest()
        {
            var logFileReader = new GamesLogFileReader(logFilePath);
            var gamesReader = logFileReader.Reader();

            using (var context = new QuakeContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Game;");
                var gamesRepository = new Games(context);
                gamesRepository.Save(gamesReader);
            }
        }

        [TestMethod]
        public void Deve_retornar_o_ranking_de_um_jogador()
        {
            var playerNameExpected = "Isgalamido";
            List<KillsByPlayers> totalRanking;

            using (var context = new QuakeContext())
            {
                var rankingOfGames = new RankingOfGames(context);
                totalRanking = rankingOfGames.FindPlayerBy(playerNameExpected);
            }

            Assert.IsTrue(totalRanking.Count > 0);
            Assert.AreEqual(playerNameExpected, totalRanking.FirstOrDefault().Name);
        }
    }
}
