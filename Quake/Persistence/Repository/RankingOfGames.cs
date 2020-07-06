using Quake.CQS;
using Quake.CQS.Contracts;
using Quake.Entities;
using Quake.Persistence.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Quake.Persistence.Repository
{
    public class RankingOfGames : IRankingOfGames
    {
        private readonly IUnitOfWork _uow;

        public RankingOfGames(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<KillsByPlayers> FindPlayerBy(string name)
        {
            using (var transaction = _uow.Current().Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var records = (
                                  from record in _uow.Current().Set<DeadPlayer>()
                                  where (name == record.Name || record.Name.Contains(name))
                                  group record by record.Name into ranking
                                  select new KillsByPlayers { Name = ranking.Key, TotalKills = ranking.Sum(x => x.TotalKills) }
                                  ).OrderByDescending(order => order.TotalKills).ToList();

                    return records;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}