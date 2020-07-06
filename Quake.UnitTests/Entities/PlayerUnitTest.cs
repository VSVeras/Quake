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
            //arrange //act
            var playerOne = PlayerFactory.Default().WithID(1).Build();

            //assert
            var IdPlayerExpected = 1;
            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
        }

        [TestMethod]
        public void Deve_criar_um_jogador_com_o_identificador_e_o_nome()
        {
            //arrange
            var IdPlayerExpected = 1;
            var namePlayerExpected = "Isgalamido";

            //act
            var playerOne = PlayerFactory.Default().WithIdAndName(IdPlayerExpected, namePlayerExpected).Build();

            //assert
            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
            Assert.AreEqual(namePlayerExpected, playerOne.Name);
        }

        [TestMethod]
        public void Deve_permitir_a_alteracao_do_nome_do_jogador()
        {
            //arrange
            var IdPlayerExpected = 1;
            var namePlayerExpected = "Isgalamido";
            var playerOne = PlayerFactory.Default().Build();

            //act
            playerOne.ChangedName("Isgalamido");

            //assert
            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
            Assert.AreEqual(namePlayerExpected, playerOne.Name);
        }
    }
}