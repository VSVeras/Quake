using Quake.Entities;
using System;

namespace Quake.UnitTests.Factories
{
    internal class PlayerFactory
    {
        private Player playerOne;

        private PlayerFactory()
        {
            playerOne = new Player(1, "Player test");
        }

        public static PlayerFactory Default()
        {
            return new PlayerFactory();
        }

        internal PlayerFactory WithID(int id)
        {
            playerOne = new Player(id);
            return this;
        }

        internal PlayerFactory WithIdAndName(int id, string name)
        {
            playerOne = new Player(id, name);
            return this;
        }

        public Player Build()
        {
            if (playerOne == null)
                throw new Exception("player was not created.");

            return playerOne;
        }
    }
}
