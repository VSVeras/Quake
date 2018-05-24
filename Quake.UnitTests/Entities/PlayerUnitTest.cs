using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.UnitTests.Factories;

namespace Quake.UnitTests.Entities
{
    [TestClass]
    public class PlayerUnitTest
    {
        [TestMethod]
        public void Deve_criar_um_jogador_somente_com_o_identificador()
        {
            var playerOne = PlayerFactory.Default().WithID(1).Build();
            var IdPlayerExpected = 1;

            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
        }

        [TestMethod]
        public void Deve_criar_um_jogador_com_o_identificador_e_o_nome()
        {
            var IdPlayerExpected = 1;
            var namePlayerExpected = "Isgalamido";
            var playerOne = PlayerFactory.Default().WithIdAndName(IdPlayerExpected, namePlayerExpected).Build();

            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
            Assert.AreEqual(namePlayerExpected, playerOne.Name);
        }

        [TestMethod]
        public void Deve_permitir_a_alteracao_do_nome_do_jogador()
        {
            var IdPlayerExpected = 1;
            var namePlayerExpected = "Isgalamido";
            var playerOne = PlayerFactory.Default().Build();

            playerOne.Changed("Isgalamido");

            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
            Assert.AreEqual(namePlayerExpected, playerOne.Name);
        }
    }
}
