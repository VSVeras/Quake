using System.Collections.Generic;
using System.Linq;

namespace Quake.Entities
{
    public class Game
    {
        private readonly List<Player> _player;
        public IEnumerable<Player> Players => _player;

        public Game()
        {
            _player = new List<Player>();
        }

        public void Add(Player player)
        {
            _player.Add(player);
        }

        public void RenamePlayer(Player playerOne, string name)
        {
            var player = _player.FirstOrDefault(atWhere => atWhere.Id == playerOne.Id);

            if (player != null)
                player.Changed(name);

        }
    }
}
