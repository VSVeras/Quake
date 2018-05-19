using System.Collections.Generic;

namespace Quake.Applications.MVVM.Games
{
    public class Game
    {
        public int Id { get; set; }
        public decimal TotalKills { get; set; }

        public List<Player> Players { get; set; }

        public List<DeadPlayer> DeadPlayers { get; set; }

        public List<KillsByMeans> KillsByMeans { get; set; }
    }
}
