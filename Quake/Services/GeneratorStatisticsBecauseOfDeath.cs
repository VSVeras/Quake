using Quake.Entities;
using Quake.Entities.Contracts;
using Quake.ValueObjects;
using System.Linq;

namespace Quake.Services
{
    public class GeneratorStatisticsBecauseOfDeath : IGeneratorStatistics
    {
        public void BecauseOfDeath(MeansOfDeath meansOfDeath, Game game)
        {
            var killsByMeans = game.KillsByMeans.FirstOrDefault(atWhere => atWhere.MeansOfDeath == meansOfDeath);
            if (killsByMeans != null)
            {
                killsByMeans.Sum();
            }
            else
            {
                var newKillsByMeans = new KillsByMeans(meansOfDeath);
                game.AddNewKill(newKillsByMeans);
            }
        }
    }
}
