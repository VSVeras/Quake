using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.Entities;
using Quake.ValueObjects;
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
        public void Deve_iniciar_um_jogo_e_permitir_a_alteracao_do_nome_do_jogador()
        {
            var IdPlayerExpected = 1;
            var namePlayerExpected = "Veras, Veranildo";
            var playerOne = new Player(1, "Veras");
            game.Add(playerOne);

            game.ChangeNameOf(playerOne, "Veras, Veranildo");

            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
            Assert.AreEqual(namePlayerExpected, playerOne.Name);
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_dois_jogadores_diferentes()
        {
            var totalPlayersExpected = 2;
            var playerOne = new Player(1, "Ana Carolina");
            var playerTwo = new Player(2, "Veras");
            game.Add(playerOne);
            game.Add(playerTwo);

            game.ChangeNameOf(playerTwo, "Veras, Veranildo");

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_dois_jogadores_diferentes_e_permitir_a_alteracao_do_nome_de_um_jogador()
        {
            var totalPlayersExpected = 2;
            var namePlayerExpected = "Veras, Veranildo";
            var playerOne = new Player(1, "Ana Carolina");
            var playerTwo = new Player(2, "Veras");
            game.Add(playerOne);
            game.Add(playerTwo);

            game.ChangeNameOf(playerTwo, "Veras, Veranildo");

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
            Assert.AreEqual(namePlayerExpected, playerTwo.Name);
        }

        [TestMethod]
        public void Deve_morer_um_jogador_por_morte_natual()
        {
            var totalOfDeadPlayersExpected = 1m;
            var playerOne = new Player(2, "Veras");
            game.Add(playerOne);

            game.KillByNaturalDeath(playerOne, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
        }

        [TestMethod]
        public void Deve_matar_um_jogador_e_atualizar_o_total_de_mortes()
        {
            var totalOfDeadPlayersExpected = 1m;
            var playerOne = new Player(1022, "world");
            var playerTwo = new Player(2, "Veras");
            game.Add(playerOne);
            game.Add(playerTwo);

            game.Kill(playerOne, playerOne, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
        }
    }
}
