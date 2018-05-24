using Quake.Entities.Contracts;

namespace Quake.Entities
{
    public class DeadPlayer : IDeadPlayer
    {
        public int IdGame { get; protected set; }
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public decimal TotalKills { get; protected set; }
        public virtual Game Game { get; protected set; }

        public DeadPlayer(int id, string name)
        {
            Id = id;
            Name = name;
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
