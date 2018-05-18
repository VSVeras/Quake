﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.Entities;
using Quake.Infrastructure.Infrastructure.Readers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quake.Infrastructure.UnitTests.Infrastructure.Readers
{
    [TestClass]
    public class GameReaderUnitTest
    {
        private List<Game> games;
        private string logFilePath = Environment.CurrentDirectory + @"\Container\games.log";

        [TestInitialize]
        public void Iniciar()
        {
            var logFileReader = new GamesLogFileReader(logFilePath);

            games = logFileReader.Reader();
        }

        [TestMethod]
        public void Deve_ler_um_arquivo_de_log_e_retornar_um_jogo()
        {
            Assert.IsTrue(games.Count > 0);
        }

        [TestMethod]
        public void Deve_ler_um_arquivo_de_log_e_retornar_um_jogo_com_um_jogador()
        {
            var PlayersExpected = new List<Player> { new Player(2, "Isgalamido"), new Player(3, "Dono da Bola") };
            var valueExpected = games.All(atWhere => atWhere.Players.Any(criterion => PlayersExpected.Any(atWhereCriterion => atWhereCriterion.Id == criterion.Id)));

            Assert.IsTrue(valueExpected);
        }
    }
}
