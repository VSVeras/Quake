using Quake.CQRS;
using Quake.CQRS.Contracts;
using Quake.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Quake.Persistence.Repository
{
    public class RankingOfGames : IRankingOfGames, IDisposable
    {
        private readonly QuakeContext context;

        public RankingOfGames(QuakeContext context)
        {
            this.context = context ?? throw new ArgumentException("The connection to the database was not reported.");
        }

        public List<KillsByPlayers> FindPlayerBy(string name)
        {
            using (var transaction = context.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    context.Configuration.LazyLoadingEnabled = false;

                    var records = (
                                  from record in context.DeadPlayer
                                  where (name == record.Name || record.Name.Contains(name))
                                  group record by record.Name into ranking
                                  select new KillsByPlayers { Name = ranking.Key, TotalKills = ranking.Sum(x => x.TotalKills) }
                                  ).ToList();

                    return records;
                }
                catch
                {
                    throw;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            context?.Dispose();
        }
    }
}
