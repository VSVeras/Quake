namespace Quake.Applications.MVVM.Games
{
    public class KillsByMeans
    {
        public int IdGame { get; set; }
        public string MeansOfDeath { get; set; }
        public decimal TotalKills { get; set; }
        public Game Game { get; set; }
    }
}
