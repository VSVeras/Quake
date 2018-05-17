using Quake.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Quake.Entities
{
    public class Game
    {
        private readonly List<Player> _player;
        public IEnumerable<Player> Players => _player;
        public decimal TotalKills { get; private set; }

        public Game()
        {
            _player = new List<Player>();
        }

        public void Add(Player player)
        {
            var onePlayer = _player.FirstOrDefault(atWhere => atWhere.Id == player.Id);

            if (onePlayer == null)
                _player.Add(player);
        }

        public void ChangeNameOf(Player player, string name)
        {
            var onePlayer = _player.FirstOrDefault(atWhere => atWhere.Id == player.Id);

            if (onePlayer != null)
                onePlayer.Changed(name);

        }

        public void Kill(Player killer, Player killed, MeansOfDeath meansOfDeath)
        {
            TotalKills++;
        }
    }
}
