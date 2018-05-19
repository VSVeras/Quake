namespace Quake.Applications.MVVM.Games
{
    public class DeadPlayer
    {
        public int IdGame { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalKills { get; set; }
        public Game Game { get; set; }
    }
}
