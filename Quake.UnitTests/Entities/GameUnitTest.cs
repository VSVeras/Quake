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
            var namePlayerExpected = "Isgalamido";
            var playerOne = new Player(1);
            game.Add(playerOne);

            game.ChangeNameOf(playerOne, "Isgalamido");

            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
            Assert.AreEqual(namePlayerExpected, playerOne.Name);
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_dois_jogadores_diferentes()
        {
            var totalPlayersExpected = 2;
            var playerOne = new Player(1, "Isgalamido");
            var playerTwo = new Player(2, "Dono da Bola");

            game.Add(playerOne);
            game.Add(playerTwo);

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_dois_jogadores_diferentes_e_permitir_a_alteracao_do_nome_de_um_dos_jogadores()
        {
            var totalPlayersExpected = 2;
            var namePlayerExpected = "Isgalamido";
            var playerOne = new Player(1, "Dono da Bola");
            var playerTwo = new Player(2);

            game.Add(playerOne);
            game.Add(playerTwo);
            game.ChangeNameOf(playerTwo, "Isgalamido");

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
            Assert.AreEqual(namePlayerExpected, playerTwo.Name);
        }

        [TestMethod]
        public void Deve_matar_um_jogador_de_morte_natual_e_atualizar_o_total_das_mortes()
        {
            var totalOfDeadPlayersExpected = 1m;
            var playerOne = new Player(2, "Isgalamido");
            game.Add(playerOne);

            game.KillByNaturalDeath(playerOne, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_e_atualizar_o_total_das_mortes()
        {
            var totalOfDeadPlayersExpected = 1m;
            var playerOne = new Player(1, "Dono da Bola");
            var playerTwo = new Player(2, "Isgalamido");
            game.Add(playerOne);
            game.Add(playerTwo);

            game.Kill(playerOne, playerOne, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
        }

        [TestMethod]
        public void Deve_matar_um_jogador_de_morte_natual_e_subtrair_uma_morte_do_total_agrupados_das_mortes()
        {
            var totalOfDeadPlayersExpected = 1m;
            var totalOfDeathsGroupedPerPlayer = 0m;
            var playerOne = new Player(2, "Isgalamido");
            game.Add(playerOne);

            game.KillByNaturalDeath(playerOne, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(playerOne));
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_e_somar_uma_morte_do_total_agrupados_das_mortes()
        {
            var totalOfDeadPlayersExpected = 1m;
            var totalOfDeathsGroupedPerPlayer = 1m;
            var playerOne = new Player(1, "Dono da Bola");
            var playerTwo = new Player(2, "Isgalamido");
            game.Add(playerOne);
            game.Add(playerTwo);

            game.Kill(playerOne, playerOne, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(playerOne));
        }

        [TestMethod]
        public void Deve_matar_um_jogador_de_morte_natual_por_duas_vezes_e_subtrair_duas_morte_do_total_agrupados_das_mortes()
        {
            var totalOfDeadPlayersExpected = 2m;
            var totalOfDeathsGroupedPerPlayer = 0m;
            var playerOne = new Player(2, "Isgalamido");
            game.Add(playerOne);

            game.KillByNaturalDeath(playerOne, MeansOfDeath.MOD_TRIGGER_HURT);
            game.KillByNaturalDeath(playerOne, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(playerOne));
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_por_duas_vezes_e_somar_duas_morte_do_total_agrupados_das_mortes()
        {
            var totalOfDeadPlayersExpected = 2m;
            var totalOfDeathsGroupedPerPlayer = 2m;
            var playerOne = new Player(1, "Dono da Bola");
            var playerTwo = new Player(2, "Isgalamido");
            game.Add(playerOne);
            game.Add(playerTwo);

            game.Kill(playerOne, playerOne, MeansOfDeath.MOD_TRIGGER_HURT);
            game.Kill(playerOne, playerOne, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(playerOne));
        }
    }
}
