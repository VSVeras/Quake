namespace Quake.Entities
{
    public class Player
    {
        public int IdGame { get; protected set; }
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public virtual Game Game { get; protected set; }

        public Player(int id)
        {
            Id = id;
        }

        public Player(int id, string name) : this(id)
        {
            Name = name;
        }

        public void Changed(string name)
        {
            Name = name;
        }
    }
}
