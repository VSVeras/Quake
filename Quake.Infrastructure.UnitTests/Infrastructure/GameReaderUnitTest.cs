using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.Infrastructure.Readers;
using System;

namespace Quake.Infrastructure.UnitTests.Infrastructure
{
    [TestClass]
    public class GameReaderUnitTest
    {
        private string logFilePath = Environment.CurrentDirectory + @"\Container\games.log";

        [TestMethod]
        public void Deve_ler_um_arquivo_de_log_e_retornar_um_jogo()
        {
            var logFileReader = new GamesLogFileReader(logFilePath);

            var games = logFileReader.Reader();

            Assert.IsTrue(games.Count > 0);
        }
    }
}
