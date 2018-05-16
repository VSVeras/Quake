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
        public void Deve_iniciar_um_jogo_sem_um_jogador()
        {
            var totalPlayersExpected = 0;

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_um_jogador()
        {
            var totalPlayersExpected = 1;

            game.Add(playerOne);

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_e_permitir_a_alteracao_do_nome_de_um_jogador()
        {
            var IdPlayerExpected = 1;
            var namePlayerExpected = "Veras, Veranildo";
            var playerOne = new Player(1, "Veras");
            game.Add(playerOne);

            game.RenamePlayer(playerOne, "Veras, Veranildo");

            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
            Assert.AreEqual(namePlayerExpected, playerOne.Name);
        }
    }
}
