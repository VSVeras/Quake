namespace Quake.Entities
{
    public class DeadPlayer
    {
        public int Id { get; protected set; }
        public Player Player { get; private set; }
        public decimal TotalKills { get; private set; }

        public DeadPlayer(Player killed)
        {
            Player = killed;
        }

        public void Subtract()
        {
            TotalKills--;
        }

        public void Sum()
        {
            TotalKills++;
        }
    }
}
