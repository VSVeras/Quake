using Quake.Entities.Contracts;
using Quake.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Quake.Entities
{
    public class Game
    {
        public int Id { get; protected set; }
        public decimal TotalKills { get; protected set; }

        public virtual List<Player> Players { get; set; }

        public virtual List<DeadPlayer> DeadPlayers { get; set; }

        public virtual List<KillsByMeans> KillsByMeans { get; set; }

        private IGeneratorStatistics GeneratorStatistics { get; }
    
        protected Game()
        {
            Players = new List<Player>();
            DeadPlayers = new List<DeadPlayer>();
            KillsByMeans = new List<KillsByMeans>();
        }

        public Game(IGeneratorStatistics generatorStatistics) : this()
        {
            GeneratorStatistics = generatorStatistics;
        }

        public void Add(Player player)
        {
            var onePlayer = FindPlayer(player.Id);
            if (onePlayer == null)
                Players.Add(player);
        }

        public void ChangeNameOf(Player player, string name)
        {
            var onePlayer = FindPlayer(player.Id);
            if (onePlayer != null)
                onePlayer.Changed(name);
        }

        private Player FindPlayer(int id)
        {
            return Players.FirstOrDefault(atWhere => atWhere.Id == id);
        }

        public void KillForMurder(Player killer, Player victim, MeansOfDeath meansOfDeath)
        {
            var deadPlayerExist = FindPlayerDead(victim.Id);
            if (deadPlayerExist != null)
            {
                deadPlayerExist.Sum();
            }
            else
            {
                AddNewDeadPlayer(victim);
            }
            TotalKills++; //total_kills são os kills dos games, isso inclui mortes do <world>.
            GeneratorStatistics.BecauseOfDeath(meansOfDeath, this);
        }

        public void AddDeathStatistics(KillsByMeans killsByMeans)
        {
            KillsByMeans.Add(killsByMeans);
        }

        private void AddNewDeadPlayer(Player victim)
        {
            var player = FindPlayer(victim.Id);
            if (player == null)
                player = victim;

            var newDeadPlayer = new DeadPlayer(player.Id, player.Name);
            newDeadPlayer.Sum();
            DeadPlayers.Add(newDeadPlayer);
        }

        //<world> não é um player e não deve aparecer na lista de players e nem no dicionário de kills.
        public void KillByNaturalDeath(Player victim, MeansOfDeath meansOfDeath)
        {
            var deadPlayerExist = FindPlayerDead(victim.Id);
            if (deadPlayerExist != null)
            {
                deadPlayerExist.Subtract(); //Quando o <world> mata o player ele perde -1 kill.
            }
            TotalKills++; //total_kills são os kills dos games, isso inclui mortes do <world>.
            GeneratorStatistics.BecauseOfDeath(meansOfDeath, this);
        }

        private DeadPlayer FindPlayerDead(int id)
        {
            return DeadPlayers.FirstOrDefault(atWhere => atWhere.Id == id);
        }

        public decimal DeathsGroupedPerPlayer(Player player)
        {
            decimal totalDeaths = TotalSumOfDeathsGroupedPerPlayer(player.Id);
            return totalDeaths;
        }

        private decimal TotalSumOfDeathsGroupedPerPlayer(int id)
        {
            var totalDeaths = 0m;
            var playerKilled = DeadPlayers.FirstOrDefault(atWhere => atWhere.Id == id);
            if (playerKilled != null)
                totalDeaths = playerKilled.TotalKills;

            return totalDeaths;
        }
    }
}
