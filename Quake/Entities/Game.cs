using Quake.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Quake.Entities
{
    public class Game
    {
        private readonly List<Player> _player;
        public IEnumerable<Player> Players => _player;
        public decimal TotalKills { get; protected set; }
        public readonly List<DeadPlayer> _deadPlayers;
        public IEnumerable<DeadPlayer> DeadPlayers => _deadPlayers;

        public Game()
        {
            _deadPlayers = new List<DeadPlayer>();
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

        public void KillForMurder(Player killer, Player victim, MeansOfDeath meansOfDeath)
        {
            var playerDeadExist = FindPlayerDead(victim.Id);
            if (playerDeadExist != null)
            {
                playerDeadExist.Sum();
            }
            else
            {
                var newPlayerDead = new DeadPlayer();
                newPlayerDead.Create(victim);
                _deadPlayers.Add(newPlayerDead);
            }
            TotalKills++;
        }


        public void KillByNaturalDeath(Player victim, MeansOfDeath mOD_TRIGGER_HURT)
        {
            var playerDeathExist = FindPlayerDead(victim.Id);
            if (playerDeathExist != null)
            {
                if (playerDeathExist.TotalKills > 0m)
                    playerDeathExist.Subtract();
            }

            TotalKills++;
        }

        private DeadPlayer FindPlayerDead(int id)
        {
            return _deadPlayers.FirstOrDefault(atWhere => atWhere.Player.Id == id);
        }

        public decimal DeathsGroupedPerPlayer(Player player)
        {
            decimal totalDeaths = TotalSumOfDeathsGroupedPerPlayer(player.Id);
            return totalDeaths;
        }


        private decimal TotalSumOfDeathsGroupedPerPlayer(int id)
        {
            var totalDeaths = 0m;
            var playerKilled = _deadPlayers.FirstOrDefault(atWhere => atWhere.Player.Id == id);
            if (playerKilled != null)
                totalDeaths = playerKilled.TotalKills;

            return totalDeaths;
        }
    }
}
