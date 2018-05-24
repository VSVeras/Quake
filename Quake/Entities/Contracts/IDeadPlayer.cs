namespace Quake.Entities.Contracts
{
    public interface IDeadPlayer
    {
        decimal TotalKills { get; }

        void Subtract();
        void Sum();
    }
}
