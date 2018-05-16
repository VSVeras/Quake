using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quake.Entities;

namespace Quake.UnitTests.Entities
{
    public class PlayerUnitTest
    {
        [TestMethod]
        public void Deve_criar_participante_somente_com_identificador()
        {
            var playerOne = new Player(1);
            var IdPlayerExpected = 1;

            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
        }

        [TestMethod]
        public void Deve_criar_um_participante_com_identificador_e_nome()
        {
            var playerOne = new Player(1, "Veras");

            var IdPlayerExpected = 1;
            var namePlayerExpected = "Veras";

            Assert.AreEqual(IdPlayerExpected, playerOne.Id);
            Assert.AreEqual(namePlayerExpected, playerOne.Name);
        }
    }
}
