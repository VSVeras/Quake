using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.Entities;
using Quake.UnitTests.Factories;
using Quake.ValueObjects;
using System.Linq;

namespace Quake.UnitTests.Entities
{
    [TestClass]
    public class GameUnitTest
    {
        private Game game;

        [TestInitialize]
        public void Iniciar()
        {
            game = new Game();
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_sem_um_jogador()
        {
            //arrange - SUT
            var totalPlayersExpected = 0;

            //act
            var totalPlayers = game.Players.Count();

            //assert
            Assert.AreEqual(totalPlayersExpected, totalPlayers);
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_um_jogador()
        {
            var totalPlayersExpected = 1;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();

            game.Add(killer);

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_e_permitir_a_alteracao_do_nome_do_jogador()
        {
            var IdPlayerExpected = 1;
            var namePlayerExpected = "Isgalamido";
            var killer = PlayerFactory.Default().WithIdAndName(IdPlayerExpected, namePlayerExpected).Build();
            game.Add(killer);

            game.ChangeNameOf(killer, "Isgalamido");

            Assert.AreEqual(IdPlayerExpected, killer.Id);
            Assert.AreEqual(namePlayerExpected, killer.Name);
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_dois_jogadores_diferentes()
        {
            var totalPlayersExpected = 2;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();

            game.Add(killer);
            game.Add(victim);

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_dois_jogadores_diferentes_e_permitir_a_alteracao_do_nome_de_um_dos_jogadores()
        {
            var totalPlayersExpected = 2;
            var namePlayerExpected = "Isgalamido";
            var killer = PlayerFactory.Default().WithIdAndName(1, "Dono da Bola").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();

            game.Add(killer);
            game.Add(victim);
            game.ChangeNameOf(victim, namePlayerExpected);

            Assert.AreEqual(totalPlayersExpected, game.Players.Count());
            Assert.AreEqual(namePlayerExpected, victim.Name);
        }

        [TestMethod]
        public void Deve_matar_um_jogador_de_morte_natual_e_atualizar_o_total_das_mortes()
        {
            var totalOfDeadPlayersExpected = 1m;
            var killer = PlayerFactory.Default().Build();
            game.Add(killer);

            game.KillByNaturalDeath(killer, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_e_atualizar_o_total_das_mortes()
        {
            var totalOfDeadPlayersExpected = 1m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);

            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
        }

        [TestMethod]
        public void Deve_matar_um_jogador_de_morte_natual_e_subtrair_uma_morte_do_total_agrupados_das_mortes()
        {
            var totalOfDeadPlayersExpected = 2m;
            var totalOfDeathsGroupedPerPlayer = 0m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);

            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(victim));
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_e_somar_uma_morte_do_total_agrupados_das_mortes()
        {
            var totalOfDeadPlayersExpected = 1m;
            var totalOfDeathsGroupedPerPlayer = 1m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);

            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(victim));
        }

        [TestMethod]
        public void Deve_matar_um_jogador_de_morte_natual_por_duas_vezes_e_subtrair_duas_morte_do_total_agrupados_das_mortes()
        {
            var totalOfDeadPlayersExpected = 2m;
            var totalOfDeathsGroupedPerPlayer = 0m;
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(victim);

            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_TRIGGER_HURT);
            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(victim));
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_por_duas_vezes_e_somar_duas_morte_do_total_agrupados_das_mortes()
        {
            var totalOfDeadPlayersExpected = 2m;
            var totalOfDeathsGroupedPerPlayer = 2m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);

            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(victim));
        }

        [TestMethod]
        public void Deve_totalizar_as_mortes_agrupados_por_tipo()
        {
            var totalMeansOfDeathExpected = 3m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);

            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_BFG_SPLASH);
            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_BFG_SPLASH);
            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_TRIGGER_HURT);

            Assert.AreEqual(totalMeansOfDeathExpected, game.KillsByMeans.Where(c => c.MeansOfDeath == MeansOfDeath.MOD_TRIGGER_HURT).Sum(field => field.TotalKills));
        }
    }
}
