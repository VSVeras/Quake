using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.Entities;
using Quake.Entities.Contracts;
using Quake.Infrastructure.Contracts;
using Quake.Infrastructure.Infrastructure.Readers;
using Quake.Persistence.Repository;
using Quake.Persistence.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quake.Infrastructure.UnitTests.Infrastructure.Readers
{
    [TestClass]
    public class GameReaderUnitTest
    {
        private static readonly string logFilePath = Environment.CurrentDirectory + @"\Container\games.log";
        private static IGamesLogFileReader logFileReader;
        private static List<Game> gamesReader;
        private static IGames gamesRepository;

        static GameReaderUnitTest()
        {
            logFileReader = new GamesLogFileReader(logFilePath);

            gamesReader = logFileReader.Reader();

            using (var uow = new UnitOfWork())
            {
                uow.Current().Database.ExecuteSqlCommand("DELETE FROM Game;");

                gamesRepository = new Games(uow);
                gamesRepository.Save(gamesReader);
            }
        }

        [TestMethod]
        public void Deve_retornar_um_ou_mais_jogos()
        {
            var totalGamesExpected = gamesReader.Count;

            Assert.IsTrue(totalGamesExpected > 0);
        }

        [TestMethod]
        public void Deve_retornar_um_jogo_com_um_jogador()
        {
            var playersExpected = new List<Player> { new Player(2, "Isgalamido"), new Player(3, "Dono da Bola") };
            var foundPlayer = gamesReader.All(atWhere => atWhere.Players.Any(criterion => playersExpected.Any(atWhereCriterion => atWhereCriterion.Id == criterion.Id)));

            Assert.IsTrue(foundPlayer);
        }

        [TestMethod]
        public void Deve_retornar_um_jogo_com_o_nome_de_um_jogador_alterado()
        {
            var playerExpected = new Player(2, "Isgalamido");
            var foundPlayer = gamesReader.Any(atWhere => atWhere.Players.Any(criterion => criterion.Id == playerExpected.Id && criterion.Name == playerExpected.Name));

            Assert.IsTrue(foundPlayer);
        }
    }
}
