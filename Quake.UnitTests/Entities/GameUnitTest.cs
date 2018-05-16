using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.Entities;
using System.Linq;

namespace Quake.UnitTests.Entities
{
    [TestClass]
    public class GameUnitTest
    {
        private Game game;
        private Player playerOne;

        [TestInitialize]
        public void Iniciar()
        {
            game = new Game();
            playerOne = new Player(1);
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_sem_participante()
        {
            var totalPlayersExpected = 0;

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_um_participante()
        {
            var totalPlayersExpected = 1;

            game.Add(playerOne);

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
        }
    }
}
