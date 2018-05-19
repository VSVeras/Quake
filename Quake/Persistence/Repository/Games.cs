using Quake.Entities;
using Quake.Entities.Contracts;
using Quake.Persistence.Database;
using System.Collections.Generic;

namespace Quake.Persistence.Repository
{
    public class Games : IGames
    {
        private readonly QuakeContext Context;

        public Games(QuakeContext context)
        {
            Context = context;
        }

        public void Save(List<Game> games)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.Game.AddRange(games);
                    Context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
