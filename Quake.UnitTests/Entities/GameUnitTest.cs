using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.Entities;
using Quake.Entities.Contracts;
using Quake.Services;
using Quake.UnitTests.Factories;
using Quake.ValueObjects;
using System.Linq;

namespace Quake.UnitTests.Entities
{
    [TestClass]
    public class GameUnitTest
    {
        private Game game;
        private IGeneratorStatistics generatorStatistics;

        [TestInitialize]
        public void Iniciar()
        {
            generatorStatistics = new GeneratorStatisticsBecauseOfDeath();
            game = new Game(generatorStatistics);
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_sem_um_jogador()
        {
            //arrange
            var totalPlayersExpected = 0;

            //act
            var totalPlayers = game.Players.AsEnumerable().Count();

            //assert
            Assert.AreEqual(totalPlayersExpected, totalPlayers);
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_um_jogador()
        {
            //arrange
            var totalPlayersExpected = 1;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();

            //act
            game.Add(killer);

            //assert
            Assert.AreEqual(totalPlayersExpected, game.Players.AsEnumerable().Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_e_permitir_a_alteracao_do_nome_do_jogador()
        {
            //arrange
            var IdPlayerExpected = 1;
            var namePlayerExpected = "Isgalamido";
            var killer = PlayerFactory.Default().WithIdAndName(IdPlayerExpected, namePlayerExpected).Build();
            game.Add(killer);

            //act
            game.ChangeNameOf(killer, "Isgalamido");

            //assert
            Assert.AreEqual(IdPlayerExpected, killer.Id);
            Assert.AreEqual(namePlayerExpected, killer.Name);
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_dois_jogadores_diferentes()
        {
            //arrange
            var totalPlayersExpected = 2;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);

            //act
            game.Add(victim);

            //assert
            Assert.AreEqual(totalPlayersExpected, game.Players.AsEnumerable().Count());
        }

        [TestMethod]
        public void Deve_iniciar_um_jogo_com_dois_jogadores_diferentes_e_permitir_a_alteracao_do_nome_de_um_dos_jogadores()
        {
            //arrange
            var totalPlayersExpected = 2;
            var namePlayerExpected = "Isgalamido";
            var killer = PlayerFactory.Default().WithIdAndName(1, "Dono da Bola").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);

            //act
            game.ChangeNameOf(victim, namePlayerExpected);

            //assert
            Assert.AreEqual(totalPlayersExpected, game.Players.AsEnumerable().Count());
            Assert.AreEqual(namePlayerExpected, victim.Name);
        }

        [TestMethod]
        public void Deve_matar_um_jogador_por_morte_natual_e_atualizar_o_total_das_mortes()
        {
            //arrange
            var totalOfDeadPlayersExpected = 1m;
            var killer = PlayerFactory.Default().Build();
            game.Add(killer);

            //act
            game.KillByNaturalDeath(killer, MeansOfDeath.MOD_TRIGGER_HURT); // Matar por morte natural

            //assert
            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_e_atualizar_o_total_das_mortes()
        {
            //arrange
            var totalOfDeadPlayersExpected = 1m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);

            //act
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT); // Matar por assassinato

            //assert
            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
        }

        [TestMethod]
        public void Deve_matar_um_jogador_por_morte_natual_e_subtrair_uma_morte_do_total_agrupados_das_mortes()
        {
            //arrange
            var totalOfDeadPlayersExpected = 2m;
            var totalOfDeathsGroupedPerPlayer = 0m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);

            //act
            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_TRIGGER_HURT);

            //assert
            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(victim));
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_e_somar_uma_morte_do_total_agrupados_das_mortes()
        {
            //arrange
            var totalOfDeadPlayersExpected = 1m;
            var totalOfDeathsGroupedPerPlayer = 1m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);

            //act
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);

            //assert
            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(victim));
        }

        [TestMethod]
        public void Deve_matar_um_jogador_por_morte_natual_por_duas_vezes_e_subtrair_duas_morte_do_total_agrupados_das_mortes()
        {
            //arrange
            var totalOfDeadPlayersExpected = 2m;
            var totalOfDeathsGroupedPerPlayer = 0m;
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(victim);
            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_TRIGGER_HURT);

            //act
            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_TRIGGER_HURT);

            //assert
            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(victim));
        }

        [TestMethod]
        public void Deve_um_jogador_matar_outro_jogador_por_duas_vezes_e_somar_duas_morte_do_total_agrupados_das_mortes()
        {
            //arrange
            var totalOfDeadPlayersExpected = 2m;
            var totalOfDeathsGroupedPerPlayer = 2m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);

            //act
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);

            //assert
            Assert.AreEqual(totalOfDeadPlayersExpected, game.TotalKills);
            Assert.AreEqual(totalOfDeathsGroupedPerPlayer, game.DeathsGroupedPerPlayer(victim));
        }

        [TestMethod]
        public void Deve_totalizar_as_mortes_agrupados_por_tipo()
        {
            //arrange
            var totalMeansOfDeathExpected = 3m;
            var killer = PlayerFactory.Default().WithIdAndName(1, "Isgalamido").Build();
            var victim = PlayerFactory.Default().WithIdAndName(2, "Dono da Bola").Build();
            game.Add(killer);
            game.Add(victim);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_TRIGGER_HURT);
            game.KillForMurder(killer, victim, MeansOfDeath.MOD_BFG_SPLASH);
            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_BFG_SPLASH);

            //act
            game.KillByNaturalDeath(victim, MeansOfDeath.MOD_TRIGGER_HURT);

            //assert
            Assert.AreEqual(totalMeansOfDeathExpected, game.KillsByMeans.Where(c => c.MeansOfDeath == MeansOfDeath.MOD_TRIGGER_HURT).Sum(field => field.TotalKills));
        }
    }
}