using Quake.ValueObjects;

namespace Quake.Entities.Contracts
{
    public interface IGeneratorStatistics
    {
        void BecauseOfDeath(MeansOfDeath meansOfDeath, Game game);
    }
}
