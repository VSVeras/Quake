namespace Quake.Entities
{
    public class DeadPlayer
    {
        public int Id { get; protected set; }
        public Player Player { get; private set; }
        public decimal TotalKills { get; private set; }

        public void Subtract()
        {
            TotalKills--;
        }

        public void Sum()
        {
            TotalKills++;
        }

        public void Create(Player killed)
        {
            Player = killed;
            Sum();
        }
    }
}
